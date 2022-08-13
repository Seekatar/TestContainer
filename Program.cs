using System.Net;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Configurations;
using static System.Console;

// var testcontainersBuilder = new TestcontainersBuilder<TestcontainersContainer>()
//   .WithImage("nginx")
//   .WithName("nginx")
//   .WithPortBinding(80)
//   .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(80));

// await using (var testcontainers = testcontainersBuilder.Build())
// {
//     await testcontainers.StartAsync();
//     WriteLine("Press enter");
//     ReadLine();
//     _ = WebRequest.Create("http://localhost:80");
// }

var dbBuilder = new TestcontainersBuilder<MsSqlTestcontainer>()
  .WithDatabase(new MsSqlTestcontainerConfiguration
  {
    Password = "Test123Friday!"
  });

await using (var testcontainers = dbBuilder.Build())
{
    await testcontainers.StartAsync();
    WriteLine("Connect with Data Source=127.0.0.1,49155;Database=master;User Id=sa;Password=Test123Friday!");
    WriteLine("Press enter to end it");
    ReadLine();
}
