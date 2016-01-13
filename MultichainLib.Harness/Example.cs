using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiChainLib.Harness
{
    public class Example
    {
        //internal async Task DoMagic()
        //{
        //    // connect to the client...
        //    var client = new MultiChainClient("192.168.40.130", 50001, false, "rpc_username", "rpc_password", "the_chain_name");

        //    // get some info back...
        //    var info = await client.GetInfoAsync();
        //    Console.WriteLine("Chain: {0}, difficulty: {1}", info.Result.ChainName, info.Result.Difficulty);
        //}

        internal async Task RunAsync()
        {
            var client = new MultiChainClient("192.168.40.130", 50001, false, "multichainrpc", "J1Bs45oEms6LRCcUw7CykoZ9ccUCTJbuwfdktk4N7M1Q", "chain_b82037073985329be60ae98e30");
//            var client = new MultiChainClient("localhost", 8911, null, null, "chain_2fb1bbf830bf49f6722abc6aae", "493bacb6e18601794a7b99bc2c444decd4e343ef9af8eabddca6d0f64bffd3b3");
            //var client = new MultiChainClient("rpc.pbjcloud.com", 443, true, null, null, "chain_4662dcf2e58c1daf3a5a2cf0e0", "23da5aecda55b1dd0613018265a35a0673f73398c571f5e295f9dd2a6ec64fd2");

            // get server info...
            Console.WriteLine("*** getinfo ***");
            var info = await client.GetInfoAsync();
            info.AssertOk();
            Console.WriteLine("Chain: {0}, difficulty: {1}", info.Result.ChainName, info.Result.Difficulty);
            Console.WriteLine();

            // help...
            Console.WriteLine("*** help ***");
            var help = await client.HelpAsync();
            help.AssertOk();
            Console.WriteLine(help.Result);
            Console.WriteLine();

            // help...
            Console.WriteLine("*** help ***");
            var helpDetail = await client.HelpAsync("getrawmempool");
            helpDetail.AssertOk();
            Console.WriteLine(helpDetail.Result);
            Console.WriteLine();

            // getgenerate...
            Console.WriteLine("*** getgenerate ***");
            var getGenerate = await client.GetGenerateAsync();
            getGenerate.AssertOk();
            Console.WriteLine(getGenerate.Result);
            Console.WriteLine();

            // gethashespersec...
            Console.WriteLine("*** gethashespersec ***");
            var getHashesPerSec = await client.GetHashesPerSecAsync();
            getHashesPerSec.AssertOk();
            Console.WriteLine(getHashesPerSec.Result);
            Console.WriteLine();

            // getmininginfo...
            Console.WriteLine("*** getmininginfo ***");
            var getMiningInfo = await client.GetMiningInfoAsync();
            getMiningInfo.AssertOk();
            Console.WriteLine(getMiningInfo.Result.Blocks + ", difficulty: " + getMiningInfo.Result.Difficulty);
            Console.WriteLine();

            // getbestblockhash...
            Console.WriteLine("*** getbestblockhash ***");
            var getBestBlockHash = await client.GetBestBlockHashAsync();
            getBestBlockHash.AssertOk();
            Console.WriteLine(getBestBlockHash.Result);
            Console.WriteLine();

            // getblockcount...
            Console.WriteLine("*** getblockcount ***");
            var getBlockCount = await client.GetBlockCountAsync();
            getBlockCount.AssertOk();
            Console.WriteLine(getBlockCount.Result);
            Console.WriteLine();

            // getblockcount...
            Console.WriteLine("*** getblockchaininfo ***");
            var getBlockchainInfo = await client.GetBlockchainInfoAsync();
            getBlockchainInfo.AssertOk();
            Console.WriteLine(getBlockchainInfo.RawJson);
            Console.WriteLine();

            // get peer info...
            Console.WriteLine("*** getpeerinfo ***");
            var peers = await client.GetPeerInfoAsync();
            peers.AssertOk();
            foreach (var peer in peers.Result)
                Console.WriteLine("{0} ({1})", peer.Addr, peer.Handshake);
            Console.WriteLine();

            // get the address that can issue assets...

            // create an asset...
            Console.WriteLine("*** issue ***");
            var assetName = "asset_" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
            Console.WriteLine("Asset name: " + assetName);
            var issueAddress = await CreateAddressAsync(client, BlockchainPermissions.Issue | BlockchainPermissions.Receive | BlockchainPermissions.Send);
            var asset = await client.IssueAsync(issueAddress, assetName, 1000000, 0.1M);
            asset.AssertOk();
            Console.WriteLine("Issue transaction ID: " + asset.Result);
            Console.WriteLine();

            // list the assets...
            while (true)
            {
                Console.WriteLine("*** listassets ***");
                var assets = await client.ListAssetsAsync();
                assets.AssertOk();
                AssetResponse found = null;
                foreach (var walk in assets.Result)
                {
                    Console.WriteLine("Name: {0}, ref: {1}", walk.Name, walk.AssetRef);

                    if (walk.Name == assetName)
                        found = walk;
                }
                Console.WriteLine();

                // have we found it?
                if (string.IsNullOrEmpty(found.AssetRef))
                {
                    Console.WriteLine("Asset is not ready - waiting (this can take 30 seconds or more)...");
                    Thread.Sleep(10000);
                }
                else
                    break;
            }

            // create an address...
            var recipient = await this.CreateAddressAsync(client, BlockchainPermissions.Send | BlockchainPermissions.Receive);

            // send with metadata...
            Console.WriteLine("*** sendwithmetadata ***");
            var bs = Encoding.UTF8.GetBytes("Hello, world.");
            var sendResult = await client.SendWithMetadataAsync(recipient, assetName, 1, bs);
            sendResult.AssertOk();
            Console.WriteLine("Send transaction ID: " + sendResult.Result);
            Console.WriteLine();

            // get it...
            Console.WriteLine("*** getrawtransaction ***");
            var retrieved = await client.GetRawTransactionVerboseAsync(sendResult.Result);
            retrieved.AssertOk();
            Console.WriteLine("ID: {0}", retrieved.Result.TxId);
            for(var index = 0; index < retrieved.Result.Data.Count; index++)
            {
                Console.WriteLine("Data: " + retrieved.Result.Data[index]);

                var retrievedBs = retrieved.Result.GetDataAsBytes(index);
                Console.WriteLine("--> " + Encoding.UTF8.GetString(retrievedBs));
            }
        }

        private async Task<string> CreateAddressAsync(MultiChainClient client, BlockchainPermissions permissions)
        {
            // create an address...
            Console.WriteLine("*** getnewaddress ***");
            var newAddress = await client.GetNewAddressAsync();
            newAddress.AssertOk();
            Console.WriteLine("New issue address: " + newAddress.Result);
            Console.WriteLine();

            // grant permissions...
            Console.WriteLine("*** grant ***");
            var perms = await client.GrantAsync(new List<string>() { newAddress.Result }, permissions);
            Console.WriteLine(perms.RawJson);
            perms.AssertOk();

            return newAddress.Result;
        }
    }
}
