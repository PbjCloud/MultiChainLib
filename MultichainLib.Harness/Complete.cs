using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiChainLib.Harness
{
    public class Complete
    {
        internal async Task RunAsync()
        {
            //var client = new MultiChainClient("192.168.40.131", 50009, false, "multichainrpc", "QWYLSyhv44EiSKFwyvRufNBcqtJyd8QJi7NUzm2xG2X", "chain_600bf49a419e7fb3fa0530de6e");
            //var client = new MultiChainClient("localhost", 8911, false, null, null, "chain_600bf49a419e7fb3fa0530de6e", "7ae614be3a222c8ef0f337504d046b46805baaa7f787381db33cb2e1f4b562e6");
            var client = new MultiChainClient("rpc.pbjcloud.com", 443, true, null, null, "chain_3c03d89be612441af8a5c148ca", "54cd2dd2edc0a87a05a7f7e7b0f13e9913cf6de3ca202df232b21d83ccc93fa2");

            var isPbj = false;

            // get info...
            Console.WriteLine("*** getinfo ***");
            var info = await client.GetInfoAsync();
            info.AssertOk();
            Console.WriteLine("Chain: {0}, difficulty: {1}", info.Result.ChainName, info.Result.Difficulty);
            Console.WriteLine();

            // get server info...
            if (isPbj)
            {
                Console.WriteLine("*** getserverinfo ***");
                var serverInfo = await client.GetServerInfoAsync();
                serverInfo.AssertOk();
                Console.WriteLine("Version: {0}", serverInfo.Result.Version);
                Console.WriteLine();
            }

            // get info...
            Console.WriteLine("*** ping ***");
            var ping = await client.PingAsync();
            ping.AssertOk();
            Console.WriteLine(ping.RawJson);
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

            // setgenerate...
            Console.WriteLine("*** setgenerate ***");
            var setGenerate = await client.SetGenerateAsync(true);
            setGenerate.AssertOk();
            Console.WriteLine(setGenerate.Result);
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

            // getnetworkhashps...
            Console.WriteLine("*** getnetworkhashps ***");
            var getNetworkHashPs = await client.GetNetworkHashPsAsync();
            getNetworkHashPs.AssertOk();
            Console.WriteLine(getNetworkHashPs.Result);
            Console.WriteLine();

            // getnetworkhashps...
            //Console.WriteLine("*** settxfee ***");
            //var setTxFee = await client.SetTxFeeAsync(0.001M);
            //setTxFee.AssertOk();
            //Console.WriteLine(setTxFee.Result);
            //Console.WriteLine();

            // getblocktemplate...
            //Console.WriteLine("*** getblocktemplate ***");
            //var getBlockTemplate = await client.GetBlockTemplateAsync();
            //getBlockTemplate.AssertOk();
            //Console.WriteLine(getBlockTemplate.Result);
            //Console.WriteLine();

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
            Console.WriteLine(getBlockchainInfo.Result.Description + ", best: " + getBlockchainInfo.Result.BestBlockHash);
            Console.WriteLine();

            // getdifficulty...
            Console.WriteLine("*** getdifficulty ***");
            var getDifficulty = await client.GetDifficultyAsync();
            getDifficulty.AssertOk();
            Console.WriteLine(getDifficulty.Result);
            Console.WriteLine();

            // get chain tips...
            Console.WriteLine("*** getchaintips ***");
            var getChainTips = await client.GetChainTipsAsync();
            getChainTips.AssertOk();
            foreach (var tip in getChainTips.Result)
                Console.WriteLine("{0}, {1}, {2}", tip.Height, tip.Hash, tip.Status);
            Console.WriteLine();

            // get mempool info...
            Console.WriteLine("*** getmempoolinfo ***");
            var getMempoolInfo = await client.GetMempoolInfoAsync();
            getMempoolInfo.AssertOk();
            Console.WriteLine(getMempoolInfo.Result.Size + ", bytes: " + getMempoolInfo.Result.Bytes);
            Console.WriteLine();

            // get block hash...
            Console.WriteLine("*** getblockhash ***");
            var getBlockHash = await client.GetBlockHashAsync(1);
            getBlockHash.AssertOk();
            Console.WriteLine(getBlockHash.Result);
            Console.WriteLine();

            // getblock...
            Console.WriteLine("*** getblock ***");
            var getBlock = await client.GetBlockAsync(getBlockHash.Result);
            getBlock.AssertOk();
            Console.WriteLine(getBlock.Result);
            Console.WriteLine();

            // getblock...
            Console.WriteLine("*** getblock (verbose) ***");
            var getBlockVerbose = await client.GetBlockVerboseAsync(getBlockHash.Result);
            getBlockVerbose.AssertOk();
            Console.WriteLine(getBlockVerbose.Result.Hash + ", nonce: " + getBlockVerbose.Result.Nonce);
            Console.WriteLine();

            // getrawmempool...
            Console.WriteLine("*** getrawmempool ***");
            var getRawMemPool = await client.GetRawMempoolAsync();
            getRawMemPool.AssertOk();
            Console.WriteLine(getRawMemPool.Result);
            Console.WriteLine();

            // getrawmempool...
            Console.WriteLine("*** getrawmempool (verbose) ***");
            var getRawMemPoolVerbose = await client.GetRawMempoolVerboseAsync();
            getRawMemPoolVerbose.AssertOk();
            Console.WriteLine(getRawMemPoolVerbose.Result);
            Console.WriteLine();

            // getblockchainpararms...
            Console.WriteLine("*** getblockchainparams ***");
            var getBlockchainParams = await client.GetBlockchainParamsAsync();
            getBlockchainParams.AssertOk();
            foreach (var key in getBlockchainParams.Result.Keys)
                Console.WriteLine("{0}: {1}", key, getBlockchainParams.Result[key]);
            Console.WriteLine();

            //// getblockchainpararms...
            //Console.WriteLine("*** getblockchainpararms (displayNames) ***");
            //var getBlockchainParams2 = await client.GetBlockchainParamsAsync(new List<string>() { "chain-protocol" });
            //getBlockchainParams2.AssertOk();
            //foreach (var key in getBlockchainParams2.Result.Keys)
            //    Console.WriteLine("{0}: {1}", key, getBlockchainParams2.Result[key]);
            //Console.WriteLine();

            // get connection count...
            Console.WriteLine("*** getconnectioncount ***");
            var getConnectionCount = await client.GetConnectionCountAsync();
            getConnectionCount.AssertOk();
            Console.WriteLine(getConnectionCount.Result);
            Console.WriteLine();

            // get network info...
            Console.WriteLine("*** getnetworkinfo ***");
            var getNetworkInfo = await client.GetNetworkInfoAsync();
            getNetworkInfo.AssertOk();
            Console.WriteLine(getNetworkInfo.Result.Version + ", subversion: " + getNetworkInfo.Result.Subversion);
            foreach (var network in getNetworkInfo.Result.Networks)
                Console.WriteLine(network.Name);
            Console.WriteLine();

            // get net totals...
            Console.WriteLine("*** getnettotals ***");
            var getNetTotals = await client.GetNetTotalsAsync();
            getNetTotals.AssertOk();
            Console.WriteLine("Received: {0}, sent: {1}:, time: {2}", getNetTotals.Result.TotalsBytesRecv, getNetTotals.Result.TotalsBytesSent, getNetTotals.Result.TimeMillis);
            Console.WriteLine();

            // get peer info...
            Console.WriteLine("*** getunconfirmedbalance ***");
            var getUnconfirmedBalance = await client.GetUnconfirmedBalanceAsync();
            getUnconfirmedBalance.AssertOk();
            Console.WriteLine(getUnconfirmedBalance.Result);
            Console.WriteLine();

            // keypool refill...
            Console.WriteLine("*** keypoolrefill ***");
            var keypoolRefill = await client.KeypoolRefillAsync();
            keypoolRefill.AssertOk();
            Console.WriteLine(keypoolRefill.RawJson);
            Console.WriteLine();

            // get wallet info...
            Console.WriteLine("*** getwalletinfo ***");
            var getWalletInfo = await client.GetWalletInfoAsync();
            getWalletInfo.AssertOk();
            Console.WriteLine(getWalletInfo.Result.WalletVersion + ", balance: " + getWalletInfo.Result.Balance);
            Console.WriteLine();

            // get added node info...
            Console.WriteLine("*** getaddednodeinfo ***");
            var getAddedNodeInfo = await client.GetAddedNodeInfoAsync();
            getAddedNodeInfo.AssertOk();
            Console.WriteLine(getAddedNodeInfo.RawJson);
            Console.WriteLine();

            // get raw change address...
            Console.WriteLine("*** getrawchangeaddress ***");
            var getRawChangeAddress = await client.GetRawChangeAddressAsync();
            getRawChangeAddress.AssertOk();
            Console.WriteLine(getRawChangeAddress.Result);
            Console.WriteLine();

            // get addresses...
            Console.WriteLine("*** getaddresses ***");
            var getAddresses = await client.GetAddressesAsync();
            getAddresses.AssertOk();
            foreach(var address in getAddresses.Result)
                Console.WriteLine(address);
            Console.WriteLine();

            // get addresses verbose...
            Console.WriteLine("*** getaddresses (verbose) ***");
            var getAddressesVerbose = await client.GetAddressesVerboseAsync();
            getAddressesVerbose.AssertOk();
            foreach (var address in getAddressesVerbose.Result)
                Console.WriteLine("{0}, mine: {1}", address.Address, address.IsMine);
            Console.WriteLine();

            // get peer info...
            Console.WriteLine("*** getpeerinfo ***");
            var peers = await client.GetPeerInfoAsync();
            peers.AssertOk();
            foreach (var peer in peers.Result)
                Console.WriteLine("{0} ({1})", peer.Addr, peer.Handshake);
            Console.WriteLine();

            // backup wallet...
            Console.WriteLine("*** backupwallet ***");
            var path = Path.GetTempFileName();
            Console.WriteLine(path);
            var backup = await client.BackupWalletAsync(path);
            backup.AssertOk();
            Console.WriteLine(backup.RawJson);
            Console.WriteLine();

            // backup wallet...
            Console.WriteLine("*** dumpwallet ***");
            path = Path.GetTempFileName();
            Console.WriteLine(path);
            var dumpWallet = await client.DumpWalletAsync(path);
            dumpWallet.AssertOk();
            Console.WriteLine(dumpWallet.RawJson);
            Console.WriteLine();

            // estimate fee...
            Console.WriteLine("*** estimatefee ***");
            var estimateFee = await client.EstimateFeeAsync(10);
            estimateFee.AssertOk();
            Console.WriteLine(estimateFee.Result);
            Console.WriteLine();

            // estimate priority...
            Console.WriteLine("*** estimatepriority ***");
            var estimatePriority = await client.EstimatePriorityAsync(10);
            estimatePriority.AssertOk();
            Console.WriteLine(estimatePriority.Result);
            Console.WriteLine();

            // get new address...
            Console.WriteLine("*** getnewaddress ***");
            var getNewAddress = await client.GetNewAddressAsync();
            getNewAddress.AssertOk();
            Console.WriteLine(getNewAddress.Result);
            Console.WriteLine();

            // validate address...
            Console.WriteLine("*** validateaddress ***");
            var validateAddress = await client.ValidateAddressAsync(getNewAddress.Result);
            validateAddress.AssertOk();
            Console.WriteLine(validateAddress.Result.Address + ", pubkey: " + validateAddress.Result.PubKey);
            Console.WriteLine();

            // get balance...
            Console.WriteLine("*** getbalance ***");
            var getBalance = await client.GetBalanceAsync();
            getBalance.AssertOk();
            Console.WriteLine(getBalance.Result);
            Console.WriteLine();

            // get balance...
            Console.WriteLine("*** gettotalbalances ***");
            var getTotalBalances = await client.GetTotalBalancesAsync();
            getTotalBalances.AssertOk();
            foreach (var balance in getTotalBalances.Result)
                Console.WriteLine("{0}, ref: {1}, qty: {2}", balance.Name, balance.AssetRef, balance.Qty);
            Console.WriteLine();

            // list accounts...
            Console.WriteLine("*** listaccounts ***");
            var listAccounts = await client.ListAccountsAsync();
            listAccounts.AssertOk();
            foreach (var key in listAccounts.Result.Keys)
                Console.WriteLine("{0}, balance: {1}", key, listAccounts.Result[key]);
            Console.WriteLine();

            // get account...
            Console.WriteLine("*** getaddressesbyaccount ***");
            var getAddressesByAccount = await client.GetAddressesByAccountAsync(null);
            getAddressesByAccount.AssertOk();
            foreach (var address in getAddressesByAccount.Result)
                Console.WriteLine(address);
            Console.WriteLine();

            // get account address...
            Console.WriteLine("*** getaccountaddress ***");
            var getAccountAddress = await client.GetAccountAddressAsync(null);
            getAccountAddress.AssertOk();
            Console.WriteLine(getAccountAddress.Result);
            Console.WriteLine();

            // get account...
            Console.WriteLine("*** getaccount ***");
            var getAccount = await client.GetAccountAsync(getAddressesByAccount.Result.First());
            getAccount.AssertOk();
            Console.WriteLine(getAccount.Result);
            Console.WriteLine();

            // get address balances...
            Console.WriteLine("*** getaddressbalances ***");
            var getAddressBalances = await client.GetAddressBalancesAsync(getAddressesByAccount.Result.First());
            getAddressBalances.AssertOk();
            foreach (var balance in getAddressBalances.Result)
                Console.WriteLine(balance);
            Console.WriteLine();

            // list address groupings...
            Console.WriteLine("*** listaddressgroupings ***");
            var listAddressGroupings = await client.ListAddressGroupingsAsync();
            listAddressGroupings.AssertOk();
            Console.Write(listAddressGroupings.RawJson);
            Console.WriteLine();

            // list unspent...
            Console.WriteLine("*** listunspent ***");
            var listUnspent = await client.ListUnspentAsync();
            listUnspent.AssertOk();
            foreach (var unspent in listUnspent.Result)
                Console.WriteLine("{0}, tx: {1}", unspent.Address, unspent.TxId);
            Console.WriteLine();

            // list lock unspent...
            Console.WriteLine("*** listlockunspent ***");
            var listLockUnspent = await client.ListLockUnspentAsync();
            listLockUnspent.AssertOk();
            foreach (var unspent in listLockUnspent.Result)
                Console.WriteLine(unspent);
            Console.WriteLine();

            // list since block...
            Console.WriteLine("*** listsinceblock ***");
            var listSinceBlock = await client.ListSinceBlockAsync(getBlockHash.Result);
            listSinceBlock.AssertOk();
            foreach (var tx in listSinceBlock.Result.Transactions)
                Console.WriteLine(tx.Address + ", hash: " + tx.BlockHash);
            Console.WriteLine();

            //// import address...
            //Console.WriteLine("*** importaddress ***");
            //var importAddress = await client.ImportAddressAsync("1RytCj4dMZvt3pR8SysvBo5ntXMxUu1gTWN9j8");
            //importAddress.AssertOk();
            //Console.WriteLine(importAddress.RawJson);
            //Console.WriteLine();

            // get received by account...
            Console.WriteLine("*** getreceivedbyaccount ***");
            var getReceivedByAccount = await client.GetReceivedByAccountAsync();
            getReceivedByAccount.AssertOk();
            Console.WriteLine(getReceivedByAccount.Result);
            Console.WriteLine();

            // list transactions...
            Console.WriteLine("*** listtransactions ***");
            var listTransactions = await client.ListTransactionsAsync();
            listTransactions.AssertOk();
            foreach (var tx in listTransactions.Result)
                Console.WriteLine("{0}, tx: {1}", tx.Address, tx.TxId);
            Console.WriteLine();

            // capture...
            var txId = listTransactions.Result.Last().TxId;

            // list transactions...
            Console.WriteLine("*** gettransaction ***");
            var getTransaction = await client.GetTransactionAsync(txId);
            getTransaction.AssertOk();
            Console.WriteLine("{0}, block time: {1}", getTransaction.Result.BlockHash, getTransaction.Result.BlockTime);
            foreach (var details in getTransaction.Result.Details)
                Console.WriteLine(details.Address + ", amount: " + details.Amount);
            Console.WriteLine();

            // get tx out...
            Console.WriteLine("*** gettxout ***");
            var getTxOut = await client.GetTxOutAsync(txId);
            getTxOut.AssertOk();
            Console.WriteLine("{0}, asm: {1}", getTxOut.Result.BestBlock, getTxOut.Result.ScriptPubKey.Asm);
            foreach (var walk in getTxOut.Result.Assets)
                Console.WriteLine("{0}, ref: {1}", walk.Name, walk.AssetRef);
            Console.WriteLine();

            // get raw transaction...
            Console.WriteLine("*** getrawtransaction ***");
            var getRawTransaction = await client.GetRawTransactionAsync(txId);
            getRawTransaction.AssertOk();
            Console.WriteLine(getRawTransaction.Result);
            Console.WriteLine();

            // decode raw transaction...
            Console.WriteLine("*** decoderawstransaction ***");
            var decodeRawTransaction = await client.DecodeRawTransactionAsync(getRawTransaction.Result);
            decodeRawTransaction.AssertOk();
            foreach (var walk in decodeRawTransaction.Result.Vin)
                Console.WriteLine(walk.TxId);
            foreach (var walk in decodeRawTransaction.Result.Vout)
                Console.WriteLine(walk.Value);
            Console.WriteLine();

            // get raw transaction...
            Console.WriteLine("*** getrawtransaction (verbose) ***");
            var getRawTransactionVerbose = await client.GetRawTransactionVerboseAsync(txId);
            getRawTransactionVerbose.AssertOk();
            foreach(var walk in getRawTransactionVerbose.Result.Data)
                Console.WriteLine(walk);
            Console.WriteLine();

            // get tx out set info...
            Console.WriteLine("*** gettxoutsetinfo ***");
            var getTxOutSetInfo = await client.GetTxOutSetInfoAsync();
            getTxOutSetInfo.AssertOk();
            Console.WriteLine("{0}, best: {1}", getTxOutSetInfo.Result.Height, getTxOutSetInfo.Result.BestBlock);
            Console.WriteLine();

            // prioritise transaction...
            Console.WriteLine("*** prioritisetransaction ***");
            var prioritiseTransaction = await client.PrioritiseTransactionAsync(txId, 10, 1);
            prioritiseTransaction.AssertOk();
            Console.WriteLine(prioritiseTransaction.RawJson);
            Console.WriteLine();

            // get asset balances...
            Console.WriteLine("*** getassetbalances ***");
            var getAssetBalances = await client.GetAssetBalancesAsync();
            getAssetBalances.AssertOk();
            foreach (var walk in getAssetBalances.Result)
                Console.WriteLine("{0}, ref: {1}, balance: {2}", walk.Name, walk.AssetRef, walk.Qty);
            Console.WriteLine();

            // list received by address...
            Console.WriteLine("*** listreceivedbyaddress ***");
            var listReceivedByAddress = await client.ListReceivedByAddressAsync();
            listReceivedByAddress.AssertOk();
            foreach(var walk in listReceivedByAddress.Result)
                Console.WriteLine("{0}, confirmations: {1}", walk.Address, walk.Confirmations);
            Console.WriteLine();

            // list received by account...
            Console.WriteLine("*** listreceivedbyaccount ***");
            var listReceivedByAccount = await client.ListReceivedByAccountAsync();
            listReceivedByAccount.AssertOk();
            foreach (var walk in listReceivedByAccount.Result)
                Console.WriteLine("{0}, confirmations: {1}", walk.Account, walk.Confirmations);
            Console.WriteLine();

            // list permissions...
            Console.WriteLine("*** listpermissions ***");
            var listPermissions = await client.ListPermissions(BlockchainPermissions.Admin);
            listPermissions.AssertOk();
            foreach (var walk in listPermissions.Result)
                Console.WriteLine(walk.Address + ", " + walk.Type);
            Console.WriteLine();

            // one, two... 
            var one = await CreateAddressAsync(client, BlockchainPermissions.Issue | BlockchainPermissions.Send | BlockchainPermissions.Receive);
            var two = await CreateAddressAsync(client, BlockchainPermissions.Send | BlockchainPermissions.Receive);
            var three = await CreateAddressAsync(client, BlockchainPermissions.Send | BlockchainPermissions.Receive);
            var four = await CreateAddressAsync(client, BlockchainPermissions.Send | BlockchainPermissions.Receive);

            // revoke...
            Console.WriteLine("*** revoke ***");
            var revoke = await client.RevokeAsync(new List<string>() { three }, BlockchainPermissions.Send);
            revoke.AssertOk();
            Console.WriteLine(revoke.Result);
            Console.WriteLine();

            // revoke...
            Console.WriteLine("*** revokefrom ***");
            var revokeFrom = await client.RevokeFromAsync(listPermissions.Result.First().Address, new List<string>() { three }, BlockchainPermissions.Receive);
            revokeFrom.AssertOk();
            Console.WriteLine(revokeFrom.Result);
            Console.WriteLine();

            // grant from...
            Console.WriteLine("*** grantfrom ***");
            var grantFrom = await client.GrantFromAsync(listPermissions.Result.First().Address, new List<string>() { two }, BlockchainPermissions.Send | BlockchainPermissions.Receive);
            grantFrom.AssertOk();
            Console.WriteLine(grantFrom.Result);
            Console.WriteLine();

            // set account...
            Console.WriteLine("*** setaccount ***");
            var setAccount = await client.SetAccountAsync(four, this.GetRandomName("account"));
            setAccount.AssertOk();
            Console.WriteLine(setAccount.Result);
            Console.WriteLine();

            // get received by address...
            Console.WriteLine("*** getreceivedbyaddress ***");
            var getReceivedByAddress = await client.GetReceivedByAddressAsync(one);
            getReceivedByAddress.AssertOk();
            Console.WriteLine(getReceivedByAddress.Result);
            Console.WriteLine();

            // dump priv key...
            Console.WriteLine("*** dumpprivkey ***");
            var dumpPrivKey = await client.DumpPrivKeyAsync(one);
            dumpPrivKey.AssertOk();
            Console.WriteLine(dumpWallet.Result);
            Console.WriteLine();

            // create multisig...
            var toUse = new List<string>();
            foreach (var address in getAddresses.Result)
            {
                toUse.Add(address);
                if (toUse.Count == 5)
                    break;
            }
            while (toUse.Count < 5)
                toUse.Add(await this.CreateAddressAsync(client, BlockchainPermissions.Receive));
            Console.WriteLine("*** createmultisig ***");
            var createMultiSig = await client.CreateMultiSigAsync(5, toUse);
            createMultiSig.AssertOk();
            Console.WriteLine("{0}, redeemScript: {1}", createMultiSig.Result.Address, createMultiSig.Result.RedeemScript);
            Console.WriteLine();

            Console.WriteLine("*** decodescript ***");
            var decodeScript = await client.DecodeScriptAsync(createMultiSig.Result.RedeemScript);
            decodeScript.AssertOk();
            foreach (var walk in decodeScript.Result.Addresses)
                Console.WriteLine(walk);
            Console.WriteLine();

            Console.WriteLine("*** addmultisigaddress ***");
            var addMultiSigAddress = await client.AddMultiSigAddressAsync(5, toUse);
            addMultiSigAddress.AssertOk();
            Console.WriteLine(addMultiSigAddress.Result);
            Console.WriteLine();

            // issue...
            Console.WriteLine("*** issue ***");
            var assetName = this.GetRandomName("asset"); 
            var issue = await client.IssueAsync(one, assetName, 1000000, 0.1M);
            Console.WriteLine(issue.Result);
            Console.WriteLine();

            // issue...
            Console.WriteLine("*** issuefrom ***");
            assetName = "asset_" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
            var issueFrom = await client.IssueFromAsync(one, two, assetName, 1000000, 0.1M);
            Console.WriteLine(issueFrom.Result);
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
                    if (walk.Name == assetName)
                        found = walk;
                }
                Console.WriteLine();

                // have we found it?
                if (string.IsNullOrEmpty(found.AssetRef))
                {
                    Console.WriteLine("Asset is not ready - waiting (this can take 30 seconds or more)...");
                    Thread.Sleep(2500);
                }
                else
                    break;
            }

            // send with meta data...
            Console.WriteLine("*** sendwithmetadata ***");
            var bs = Encoding.UTF8.GetBytes("Hello, world.");
            var sendWithMetaData = await client.SendWithMetadataAsync(two, assetName, 1, bs);
            sendWithMetaData.AssertOk();
            Console.WriteLine("Send transaction ID: " + sendWithMetaData.Result);
            Console.WriteLine();

            // send with meta data...
            Console.WriteLine("*** sendwithmetadataform ***");
            var sendWithMetaDataFrom = await client.SendWithMetadataFromAsync(two, one, assetName, 1, bs);
            sendWithMetaDataFrom.AssertOk();
            Console.WriteLine("Send transaction ID: " + sendWithMetaDataFrom.Result);
            Console.WriteLine();

            // send asset to address...
            Console.WriteLine("*** sendassettoaddress ***");
            var sendAssetToAddress = await client.SendAssetToAddressAsync(two, assetName, 1);
            sendAssetToAddress.AssertOk();
            Console.WriteLine("Send transaction ID: " + sendAssetToAddress.Result);
            Console.WriteLine();

            // send to address...
            Console.WriteLine("*** sendtoaddress ***");
            var sendToAddress = await client.SendToAddressAsync(two, assetName, 1);
            sendToAddress.AssertOk();
            Console.WriteLine("Send transaction ID: " + sendToAddress.Result);
            Console.WriteLine();

            // send asset from...
            Console.WriteLine("*** sendassetfrom ***");
            var sendAssetFrom = await client.SendAssetFromAsync(two, one, assetName, 1);
            sendAssetFrom.AssertOk();
            Console.WriteLine("Send transaction ID: " + sendAssetFrom.Result);
            Console.WriteLine();

            // send from...
            //Console.WriteLine("*** sendfrom ***");
            //var sendFrom = await client.SendFromAsync("", two, 1);
            //sendFrom.AssertOk();
            //Console.WriteLine(sendFrom.Result);

            // submit block...
            //Console.WriteLine("*** submitblock ***");
            //var submitBlock = await client.SubmitBlockAsync(Encoding.UTF8.GetBytes("Hello, world."));
            //submitBlock.AssertOk();
            //Console.WriteLine("Send transaction ID: " + sendAssetFrom.Result);
            //Console.WriteLine();

            //// verify message...
            //Console.WriteLine("*** verifymessage ***");
            //var sendAssetFrom = await client.SendAssetFromAsync(two, one, assetName, 1);
            //sendAssetFrom.AssertOk();
            //Console.WriteLine("Send transaction ID: " + sendAssetFrom.Result);
            //Console.WriteLine();

            // check blocks...
            Console.WriteLine("*** verifychain ***");
            var verifyChain = await client.VerifyChainAsync(CheckBlockType.ReadFromDisk);
            verifyChain.AssertOk();
            Console.WriteLine(verifyChain.Result);
            Console.WriteLine();
        }

        private string GetRandomName(string name)
        {
            return name + "_" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
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
