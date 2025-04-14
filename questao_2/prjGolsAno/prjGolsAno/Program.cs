using prjGolsAno.Services;
static class Program {
    async static Task Main(string[] args) {
        var futebolService = new FutebolService();

        Console.WriteLine("Dados estão sendo calculados...");

        await ExibirGolsTime("Paris Saint-Germain", 2013, futebolService);
        await ExibirGolsTime("Chelsea", 2014, futebolService);

        Console.WriteLine("Aperte qualquer tecla para fechar o programa.");
        Console.ReadKey();
    }

    private static async Task ExibirGolsTime(string time, int ano, FutebolService futebolService) {
        try {
            int gols = await futebolService.GolsTotaisTimeAnoAsync(time, ano);
            Console.WriteLine($"Time {time} marcou {gols} gols no ano {ano}");
        } catch (Exception ex) {
            Console.WriteLine($"Falha ao buscar gols do time {time} no ano {ano}: {ex.GetBaseException().Message}");
        }
    }
}