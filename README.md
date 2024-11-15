# SMS Rate Limiter Service

## Overview

The **SMS Rate Limiter Service** is a .NET Core microservice designed to enforce rate limits on SMS message sending. It ensures that businesses do not exceed:
- **Per-Number Rate Limit**: Maximum messages allowed per second per phone number.
- **Account-Wide Rate Limit**: Maximum messages allowed per second across all phone numbers.

The service includes:
1. A POST endpoint to check if an SMS message can be sent.
2. Unit tests to validate the functionality under various scenarios.

---

## Prerequisites

- **.NET 8 SDK**: Ensure you have .NET 8 SDK installed. You can download it [here](https://dotnet.microsoft.com/download/dotnet/8.0).
- **Postman**: Download [Postman](https://www.postman.com/downloads/) for manual API testing.

---

## Setup and Running the Application

### Clone the Repository
```bash
git clone https://github.com/nkhanna404/TapMangoSubmission.git
cd SmsRateLimiterService
```

### Configure Rate Limits
Open `appsettings.json` in the `SmsRateLimiterService` directory and adjust the rate limits:
```json
"RateLimits": {
  "MessagesPerSecondPerNumber": 5,
  "MessagesPerSecondAccount": 50
}
```

### Restore Packages and Build
Restore dependencies and build the project:
```bash
dotnet restore
dotnet build
```

### Run the Application
Start the application using:
```bash
dotnet run
```

By default, the application runs at:
```
http://localhost:5000
```

---

## Testing the Application

### Testing with Postman

1. **Launch Postman**.
2. **Create a New Request**:
   - **Method**: `POST`
   - **URL**: `http://localhost:5000/api/message/check-eligibility`
3. **Set the Request Body**:
   - Go to the **Body** tab.
   - Select **raw** and **JSON** as the format.
   - Provide a JSON payload, e.g.:
     ```json
     {
       "BusinessPhoneNumber": "123-456-7890",
       "MessageContent": "Hello, this is a test message."
     }
     ```
4. **Send the Request**:
   - Click **Send**.
   - **Expected Responses**:
     - `200 OK` with message:
       ```json
       {
         "Message": "Message can be sent."
       }
       ```
     - `400 Bad Request` with message:
       ```json
       {
         "Message": "Message limit exceeded. Cannot send message."
       }
       ```

#### Testing Scenarios
- **Normal Operation**: Send a single request to confirm messages can be sent.
- **Exceed Per-Number Limit**: Quickly send multiple requests for the same phone number (e.g., 6 requests if the limit is 5 per second).
- **Exceed Account-Wide Limit**: Send requests from multiple phone numbers until the account-wide limit is reached.

---

### Running Unit Tests with xUnit

The `RateLimiterService.Tests` project contains xUnit tests to validate the application’s functionality.

#### Steps to Run Tests

1. **Navigate to the Test Project**:
   ```bash
   cd RateLimiterService.Tests
   ```

2. **Run All Tests**:
   ```bash
   dotnet test
   ```

3. **Expected Output**:
   You should see a summary of test results, e.g.:
   ```
   Passed!  -  All tests passed successfully.
   ```

#### Included Test Cases

- **Within Limit Test**: Ensures messages can be sent if limits are not exceeded.
- **Exceed Per-Number Limit Test**: Verifies that requests exceeding the per-number rate limit are blocked.
- **Exceed Account-Wide Limit Test**: Confirms that account-wide limits are respected.

---

## Troubleshooting

### 400 Bad Request on All Requests
- Ensure the `BusinessPhoneNumber` and `MessageContent` fields are present and correctly formatted in the request payload.

### Application Not Accessible
- Confirm the application is running by checking the console output after running `dotnet run`.
- Verify the URL and port in `launchSettings.json` or specify the URL manually:
  ```bash
  dotnet run --urls="http://localhost:5000"
  ```

### Unit Tests Not Detected
- Ensure xUnit is properly installed. Check the test project’s `.csproj` file for:
  ```xml
  <PackageReference Include="xunit" Version="2.4.1" />
  <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3" />
  ```

---

## Summary

This README provides detailed instructions on how to set up, run, and test the SMS Rate Limiter Service. Use Postman for manual testing, and xUnit for automated test validation.

Let me know if you need further clarification or additional steps!
