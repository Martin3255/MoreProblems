Stack<int> whiteTiles = new Stack<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
Queue<int> greyTiles = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());

Dictionary<string, int> possibleLocations = new Dictionary<string, int>()
{
    {"Sink", 40},
    {"Oven", 50},
    {"Countertop", 60},
    {"Wall", 70},
};

Dictionary<string, int> locationsOftiles = new Dictionary<string, int>();

while (true)
{
    if (!whiteTiles.Any() || !greyTiles.Any())
    {
        break;
    }

    bool isFitting = false;
    int currWhiteTile = whiteTiles.Peek();
    int currGreytile = greyTiles.Peek();

    if (currWhiteTile == currGreytile)
    {
        int newCurrTile = currGreytile + currWhiteTile;
        foreach (var location in possibleLocations)
        {
            if (location.Value == newCurrTile)
            {
                if (!locationsOftiles.ContainsKey(location.Key))
                {
                    locationsOftiles.Add(location.Key, 1);
                }
                else
                {
                    locationsOftiles[location.Key]++;
                }
                isFitting = true;
                break;
            }
        }
        if (!isFitting)
        {
            if (!locationsOftiles.ContainsKey("Floor"))
            {
                locationsOftiles.Add("Floor", 1);
            }
            else
            {
                locationsOftiles["Floor"]++;
            }
        }
        whiteTiles.Pop();
        greyTiles.Dequeue();
    }
    else
    {
        whiteTiles.Push(whiteTiles.Pop() / 2);
        greyTiles.Enqueue(greyTiles.Dequeue());
    }
}
var whiteTilesLeft = whiteTiles.Count == 0 ? "none" : string.Join(", ", whiteTiles);
var greyTilesLeft = greyTiles.Count == 0 ? "none" : string.Join(", ", greyTiles);
Console.WriteLine($"White tiles left: {whiteTilesLeft}");
Console.WriteLine($"Grey tiles left: {greyTilesLeft}");
foreach (var item in locationsOftiles.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}