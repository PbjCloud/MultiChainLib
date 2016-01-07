## MultiChainLib -- C# library for the MultiChain private blockchain

Refer to the API documentation at http://www.multichain.com/developers/json-rpc-api/ for details of the calls.

Example:
```C#
internal async Task DoMagic()
{
    // connect to the client...
    var client = new MultiChainClient("192.168.40.130", 50001, false, 
		"rpc_username", "rpc_password", "the_chain_name");

    // get some info back...
    var info = await client.GetInfoAsync();
    Console.WriteLine("Chain: {0}, difficulty: {1}", info.Result.ChainName, info.Result.Difficulty);
}
```