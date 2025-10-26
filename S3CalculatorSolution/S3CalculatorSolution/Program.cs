// Create a WebApplication builder (used to configure web server and middleware)
var builder = WebApplication.CreateBuilder(args);

// Build the application object (actual web app)
var app = builder.Build();


// This is a "terminating middleware" — it handles the request directly
// and doesn’t call any next middleware.
app.Run(async (HttpContext context) =>
{
    // Check if request is a GET request to the root URL "/"
    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        // Declare variables for inputs and result
        int firstNumber = 0, secondNumber = 0;
        string? operation = null;  // can be "add", "subtract", etc.
        long? result = null;       // nullable long to store the operation result

        //------------------------------------------------------------
        // 1️⃣ Read 'firstNumber' from the query string (URL parameters)
        //------------------------------------------------------------
        if (context.Request.Query.ContainsKey("firstNumber"))   // check if it exists
        {
            string firstNumberString = context.Request.Query["firstNumber"][0];  // get the first value

            if (!string.IsNullOrEmpty(firstNumberString))       // check if not empty
            {
                // Convert string to integer and store it
                firstNumber = Convert.ToInt32(firstNumberString);
            }
            else
            {
                // If value exists but is empty -> invalid input
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
            }
        }
        else
        {
            // If 'firstNumber' key not present at all in query
            if (context.Response.StatusCode == 200)             // Only if not already set to error
                context.Response.StatusCode = 400;              // Set as bad request
            await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
        }


        //------------------------------------------------------------
        // 2️⃣ Read 'secondNumber' from the query string
        //------------------------------------------------------------
        if (context.Request.Query.ContainsKey("secondNumber"))
        {
            string secondNumberString = context.Request.Query["secondNumber"][0];

            if (!string.IsNullOrEmpty(secondNumberString))
            {
                // Convert string to integer
                secondNumber = Convert.ToInt32(context.Request.Query["secondNumber"][0]);
            }
            else
            {
                // If the parameter exists but has an empty value
                if (context.Response.StatusCode == 200)
                    context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
            }
        }
        else
        {
            // If 'secondNumber' not found in the query
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
        }


        //------------------------------------------------------------
        // 3️⃣ Read 'operation' from the query string
        //------------------------------------------------------------
        if (context.Request.Query.ContainsKey("operation"))
        {
            // Get the operation name (add / subtract / multiply / divide / mod)
            operation = Convert.ToString(context.Request.Query["operation"][0]);

            //--------------------------------------------------------
            // Perform the calculation based on the operation
            //--------------------------------------------------------
            switch (operation)
            {
                case "add":
                    result = firstNumber + secondNumber;
                    break;

                case "subtract":
                    result = firstNumber - secondNumber;
                    break;

                case "multiply":
                    result = firstNumber * secondNumber;
                    break;

                case "divide":
                    // Prevent DivideByZeroException by checking denominator
                    result = (secondNumber != 0) ? firstNumber / secondNumber : 0;
                    break;

                case "mod":
                    // Prevent DivideByZeroException again
                    result = (secondNumber != 0) ? firstNumber % secondNumber : 0;
                    break;
            }

            //--------------------------------------------------------
            // If a valid operation was matched and result was set
            //--------------------------------------------------------
            if (result.HasValue)
            {
                // Send the result back to the browser/client
                await context.Response.WriteAsync(result.Value.ToString());
            }
            else
            {
                // Operation parameter exists but value is invalid
                if (context.Response.StatusCode == 200)
                    context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'operation'\n");
            }

        }
        else
        {
            // 'operation' parameter missing completely
            if (context.Response.StatusCode == 200)
                context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid input for 'operation'\n");
        }
    }
});

// Start the web server and begin listening for requests
app.Run();
