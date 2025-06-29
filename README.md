✅ 1. Install .NET 9 SDK
Download and install the .NET 9 SDK from the official site:
👉 https://dotnet.microsoft.com/en-us/download/dotnet/9.0

After installation, verify the installation by running:

dotnet --version
You should see a version starting with 9.

✅ 2. Update the Database (Using Visual Studio Package Manager Console)
To apply migrations and update the database, follow these steps:

Open Visual Studio

Go to Tools → NuGet Package Manager → Package Manager Console

Run the following command:

Update-Database -Project MiniAccountManagement.Web.Infrastructure -StartupProject MiniAccountManagement.Web



Author: Md Abdur Rashed
Email: rashedroni3@gmail.com
Phone: 01882332451
