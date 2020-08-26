**dotnet-core-xunit-webdriver-restsharp-ui-api-testing-framework**
solution is running with:
- DOTNET Core 3.1, C#
- RestSharp
- FluentAssertions,
- Webdriver, Page Object Pattern
- xUnit
- MailKit

Solution does not have any static code analysis tools, use what you prefer.

---

## After repo clone, do if needed:
- dotnet restore
- dotnet build

---

## API tests explanation
- It uses RestSharp client. In case you have NSwag clients, it is preffered to use those installed from NuGet.
- It makes things much simplier and requires some refactoring of ApiClient, but that is another story.
- It contains only one API test against dummy API, and that is the reason it fails - on GET user back after POST.

---

## UI tests explanation
- It has one special test which is using IMAP client to read Gmail,
you need to provide correct username and password in order to have it working.
- UI tests are able to run in Chrome, Firefox and Edge(Chromium Edge) and different locales.
- In order to have webdriver executions published to working folder,
there are conditional compillation symbols set on build properties of UITests project:
_PUBLISH_CHROMEDRIVER;_PUBLISH_GECKODRIVER;_PUBLISH_MSEDGEDRIVER
