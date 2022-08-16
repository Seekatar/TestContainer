using System.Net;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Configurations;
using static System.Console;


await Redis();
// await Sql();
// await Kafka();

async Task Sql()
{
    var dbBuilder = new DotNet.Testcontainers.Builders.TestcontainersBuilder<DotNet.Testcontainers.Containers.MsSqlTestcontainer>();
    var config = new DotNet.Testcontainers.Configurations.MsSqlTestcontainerConfiguration();
    config.Password = "Test123Friday!";
    var dbBuilder2 = DotNet.Testcontainers.Builders.TestcontainersBuilderDatabaseExtension.WithDatabase(dbBuilder, config);

    await using (var testcontainers = dbBuilder2.Build())
    {
        await testcontainers.StartAsync();
        // Server=localhost,49159;Database=master;User Id=sa;Password=Test123Friday!; << doesn't work with localhost
        // Server=127.0.0.1,49159;Database=master;User Id=sa;Password=Test123Friday!;
        WriteLine($"Started SQL Server. Connect with {testcontainers.ConnectionString}");
        WriteLine("Press enter to end it");
        ReadLine();
    }
}

async Task Redis()
{
    var dbBuilder = new DotNet.Testcontainers.Builders.TestcontainersBuilder<DotNet.Testcontainers.Containers.RedisTestcontainer>();
    var config = new DotNet.Testcontainers.Configurations.RedisTestcontainerConfiguration();
    var dbBuilder2 = DotNet.Testcontainers.Builders.TestcontainersBuilderDatabaseExtension.WithDatabase(dbBuilder, config);

    await using (var testcontainers = dbBuilder2.Build())
    {
        await testcontainers.StartAsync();
        WriteLine($"Started Redis. Connect with {testcontainers.ConnectionString}");
        // localhost:49161
        WriteLine("Press enter to end it");
        ReadLine();
    }
}

// async Task Kafka2(){
//     await Task.Delay(1);

//     KafkaTestcontainerConfiguration configuration = new KafkaTestcontainerConfiguration();

//       var kafkaBuilder1 = new DotNet.Testcontainers.Builders.TestcontainersBuilder<KafkaTestcontainer>();
//       var kafkaBuilder = kafkaBuilder1.WithKafka(configuration);

//     kafkaBuilder.Build();

// }

async Task Kafka()
{
    var kafkaBuilder = new DotNet.Testcontainers.Builders.TestcontainersBuilder<DotNet.Testcontainers.Containers.KafkaTestcontainer>();
    var config = new KafkaTestcontainerConfiguration();
    var kafkaBuilder2 = kafkaBuilder.WithKafka(config);


    await using (var testcontainers = kafkaBuilder2.Build())
    {
        await testcontainers.StartAsync();
        WriteLine($"Started Kafka. Connect with {testcontainers.BootstrapServers}");
        // localhost:49158
        WriteLine("Press enter to end it");
        ReadLine();
    }

}