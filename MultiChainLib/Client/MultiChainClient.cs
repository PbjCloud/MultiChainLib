using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MultiChainLib
{
    public class MultiChainClient
    {
        private string Hostname { get; set; }
        private int Port { get; set; }
        private bool UseSsl { get; set; }
        private string ChainName { get; set; }
        private string ChainKey { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }

        public event EventHandler<EventArgs<JsonRpcRequest>> Executing;

        public MultiChainClient(string hostname, int port, bool useSsl, string username, string password, string chainName, string chainKey = null)
        {
            this.Hostname = hostname;
            this.Port = port;
            this.UseSsl = useSsl;
            this.Username = username;
            this.Password = password;
            this.ChainName = chainName;
            this.ChainKey = chainKey;
        }

        public Task<JsonRpcResponse<List<PeerResponse>>> GetPeerInfoAsync()
        {
            return this.ExecuteAsync<List<PeerResponse>>("getpeerinfo", 0);
        }

        public Task<JsonRpcResponse<GetInfoResponse>> GetInfoAsync()
        {
            return this.ExecuteAsync<GetInfoResponse>("getinfo", 0);
        }

        // only supported by PBJ Cloud...
        public Task<JsonRpcResponse<GetServerInfoResponse>> GetServerInfoAsync()
        {
            return this.ExecuteAsync<GetServerInfoResponse>("getserverinfo", 0);
        }

        public Task<JsonRpcResponse<string>> SendWithMetadataAsync(string address, string assetName, decimal amount, byte[] dataHex)
        {
            var theAmount = new Dictionary<string, object>();
            theAmount[assetName] = amount;
            return this.ExecuteAsync<string>("sendwithmetadata", 0, address, theAmount, FormatHex(dataHex));
        }

        public Task<JsonRpcResponse<string>> SendToAddressAsync(string address, string assetName, decimal amount, string comment = null,
            string commentTo = null)
        {
            var theAmount = new Dictionary<string, object>();
            theAmount[assetName] = amount;
            return this.ExecuteAsync<string>("sendtoaddress", 0, address, theAmount, comment ?? string.Empty, 
                commentTo ?? string.Empty);
        }

        public Task<JsonRpcResponse<string>> SendWithMetadataFromAsync(string fromAddress, string toAddress, string assetName, decimal amount, byte[] dataHex)
        {
            var theAmount = new Dictionary<string, object>();
            theAmount[assetName] = amount;
            return this.ExecuteAsync<string>("sendwithmetadatafrom", 0, fromAddress, toAddress, theAmount, FormatHex(dataHex));
        }

        public Task<JsonRpcResponse<string>> SendAssetToAddressAsync(string address, string assetName, decimal quantity, 
            int nativeAmount = 0, string comment = null, string commentTo = null)
        {
            return this.ExecuteAsync<string>("sendassettoaddress", 0, address, assetName, quantity, nativeAmount,
                comment ?? string.Empty, commentTo ?? string.Empty);
        }

        public Task<JsonRpcResponse<string>> SendAssetFromAsync(string fromAddress, string toAddress, string assetName, decimal quantity,
            int nativeAmount = 0, string comment = null, string commentTo = null)
        {
            return this.ExecuteAsync<string>("sendassetfrom", 0, fromAddress, toAddress, assetName, quantity, nativeAmount,
                comment ?? string.Empty, commentTo ?? string.Empty);
        }

        public Task<JsonRpcResponse<bool>> GetGenerateAsync()
        {
            return this.ExecuteAsync<bool>("getgenerate", 0);
        }

        public Task<JsonRpcResponse<string>> SetGenerateAsync(bool generate)
        {
            return this.ExecuteAsync<string>("setgenerate", 0, generate);
        }


        public Task<JsonRpcResponse<int>> GetHashesPerSecAsync()
        {
            return this.ExecuteAsync<int>("gethashespersec", 0);
        }

        public Task<JsonRpcResponse<MiningInfoResponse>> GetMiningInfoAsync()
        {
            return this.ExecuteAsync<MiningInfoResponse>("getmininginfo", 0);
        }

        public Task<JsonRpcResponse<string>> GetBestBlockHashAsync()
        {
            return this.ExecuteAsync<string>("getbestblockhash", 0);
        }

        public Task<JsonRpcResponse<int>> GetBlockCountAsync()
        {
            return this.ExecuteAsync<int>("getblockcount", 0);
        }

        public Task<JsonRpcResponse<BlockchainInfoResponse>> GetBlockchainInfoAsync()
        {
            return this.ExecuteAsync<BlockchainInfoResponse>("getblockchaininfo", 0);
        }

        public Task<JsonRpcResponse<decimal>> GetDifficultyAsync()
        {
            return this.ExecuteAsync<decimal>("getdifficulty", 0);
        }

        public Task<JsonRpcResponse<List<ChainTipResponse>>> GetChainTipsAsync()
        {
            return this.ExecuteAsync<List<ChainTipResponse>>("getchaintips", 0);
        }

        public Task<JsonRpcResponse<List<object>>> GetRawMempoolAsync()
        {
            return this.ExecuteAsync<List<object>>("getrawmempool", 0);
        }

        public Task<JsonRpcResponse<MempoolResponse>> GetRawMempoolVerboseAsync()
        {
            return this.ExecuteAsync<MempoolResponse>("getrawmempool", 0, true);
        }

        public Task<JsonRpcResponse<MempoolInfoResponse>> GetMempoolInfoAsync()
        {
            return this.ExecuteAsync<MempoolInfoResponse>("getmempoolinfo", 0);
        }

        public Task<JsonRpcResponse<string>> GetBlockHashAsync(int block)
        {
            return this.ExecuteAsync<string>("getblockhash", 0, block);
        }

        public Task<JsonRpcResponse<string>> GetBlockAsync(string hash)
        {
            return this.ExecuteAsync<string>("getblock", 0, hash, false);
        }

        public Task<JsonRpcResponse<BlockResponse>> GetBlockVerboseAsync(string hash)
        {
            return this.ExecuteAsync<BlockResponse>("getblock", 0, hash, true);
        }

        public static byte[] ParseHexString(string hex)
        {
            var bs = new List<byte>();
            for (var index = 0; index < hex.Length; index += 2)
                bs.Add(byte.Parse(hex.Substring(index, 2), NumberStyles.HexNumber));
            return bs.ToArray();
        }

        public Task<JsonRpcResponse<List<ListPermissionsResponse>>> ListPermissions(BlockchainPermissions permissions)
        {
            var permissionsAsString = this.FormatPermissions(permissions);
            return this.ExecuteAsync<List<ListPermissionsResponse>>("listpermissions", 0, permissionsAsString);
        }

        public Task<JsonRpcResponse<string>> IssueAsync(string issueAddress, string assetName, int quantity, decimal units, 
            decimal nativeAmount = 0, string comment = null, string commentTo = null, int startBlock = 0, int endBlock = 0)
        {
            return this.ExecuteAsync<string>("issue", 0, issueAddress, assetName, quantity, units); /*, nativeAmount, comment,
                commentTo, startBlock, endBlock);*/
        }

        public Task<JsonRpcResponse<string>> IssueFromAsync(string fromAddress, string toAddress, string assetName, int quantity, decimal units,
            decimal nativeAmount = 0, string comment = null, string commentTo = null, int startBlock = 0, int endBlock = 0)
        {
            return this.ExecuteAsync<string>("issuefrom", 0, fromAddress, toAddress, assetName, quantity, units); /*, nativeAmount, comment,
                commentTo, startBlock, endBlock);*/
        }

        public Task<JsonRpcResponse<List<AssetResponse>>> ListAssetsAsync()
        {
            return this.ExecuteAsync<List<AssetResponse>>("listassets", 0);
        }

        public Task<JsonRpcResponse<string>> GrantAsync(IEnumerable<string> addresses, BlockchainPermissions permissions, decimal nativeAmount = 0M, string comment = null,
            string commentTo = null, int startBlock = 0, int endBlock = 0)
        {
            var stringifiedAddresses = this.StringifyValues(addresses);
            var permissionsAsString = this.FormatPermissions(permissions);
            return this.ExecuteAsync<string>("grant", 0, stringifiedAddresses, permissionsAsString); /*, nativeAmount, comment ?? string.Empty, 
                commentTo ?? string.Empty, startBlock, endBlock);*/
        }

        public Task<JsonRpcResponse<string>> RevokeAsync(IEnumerable<string> addresses, BlockchainPermissions permissions, decimal nativeAmount = 0M, string comment = null,
            string commentTo = null, int startBlock = 0, int endBlock = 0)
        {
            var stringifiedAddresses = this.StringifyValues(addresses);
            var permissionsAsString = this.FormatPermissions(permissions);
            return this.ExecuteAsync<string>("revoke", 0, stringifiedAddresses, permissionsAsString); /*, nativeAmount, comment ?? string.Empty, 
                commentTo ?? string.Empty, startBlock, endBlock);*/
        }

        public Task<JsonRpcResponse<string>> GrantFromAsync(string fromAddress, IEnumerable<string> toAddresses, BlockchainPermissions permissions, decimal nativeAmount = 0M, 
            string comment = null, string commentTo = null, int startBlock = 0, int endBlock = 0)
        {
            var stringifiedAddresses = this.StringifyValues(toAddresses);
            var permissionsAsString = this.FormatPermissions(permissions);
            return this.ExecuteAsync<string>("grantfrom", 0, fromAddress, stringifiedAddresses, permissionsAsString); /*, nativeAmount, comment ?? string.Empty, 
                commentTo ?? string.Empty, startBlock, endBlock);*/
        }

        public Task<JsonRpcResponse<string>> RevokeFromAsync(string fromAddress, IEnumerable<string> toAddresses, BlockchainPermissions permissions, decimal nativeAmount = 0M,
            string comment = null, string commentTo = null, int startBlock = 0, int endBlock = 0)
        {
            var stringifiedAddresses = this.StringifyValues(toAddresses);
            var permissionsAsString = this.FormatPermissions(permissions);
            return this.ExecuteAsync<string>("revokefrom", 0, fromAddress, stringifiedAddresses, permissionsAsString); /*, nativeAmount, comment ?? string.Empty, 
                commentTo ?? string.Empty, startBlock, endBlock);*/
        }

        private string FormatPermissions(BlockchainPermissions permissions)
        {
            StringBuilder builder = new StringBuilder();
            if ((int)(permissions & BlockchainPermissions.Connect) != 0)
                builder.Append("connect");
            if ((int)(permissions & BlockchainPermissions.Send) != 0)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.Append("send");
            }
            if ((int)(permissions & BlockchainPermissions.Receive) != 0)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.Append("receive");
            }
            if ((int)(permissions & BlockchainPermissions.Issue) != 0)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.Append("issue");
            }
            if ((int)(permissions & BlockchainPermissions.Mine) != 0)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.Append("mine");
            }
            if ((int)(permissions & BlockchainPermissions.Admin) != 0)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.Append("admin");
            }

            return builder.ToString();
        }

        public Task<JsonRpcResponse<string>> GetRawTransactionAsync(string txId)
        {
            return this.ExecuteAsync<string>("getrawtransaction", 0, txId, 0);
        }

        public Task<JsonRpcResponse<VerboseTransactionResponse>> DecodeRawTransactionAsync(string data)
        {
            return this.ExecuteAsync<VerboseTransactionResponse>("decoderawtransaction", 0, data);
        }

        public Task<JsonRpcResponse<VerboseTransactionResponse>> GetRawTransactionVerboseAsync(string txId)
        {
            return this.ExecuteAsync<VerboseTransactionResponse>("getrawtransaction", 0, txId, 1);
        }

        private string StringifyValues(IEnumerable<string> values)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var address in values)
            {
                if (builder.Length > 0)
                    builder.Append(",");
                builder.Append(address);
            }
            return builder.ToString();
        }

        public Task<JsonRpcResponse<string>> GetNewAddressAsync()
        {
            return this.ExecuteAsync<string>("getnewaddress", 0);
        }

        public Task<JsonRpcResponse<Dictionary<string, object>>> GetBlockchainParamsAsync()
        {
            return this.ExecuteAsync<Dictionary<string, object>>("getblockchainparams", 0);
        }

        public Task<JsonRpcResponse<Dictionary<string, object>>> GetBlockchainParamsAsync(bool displayNames)
        {
            return this.ExecuteAsync<Dictionary<string, object>>("getblockchainparams", 0, displayNames);
        }

        public Task<JsonRpcResponse<int>> GetConnectionCountAsync()
        {
            return this.ExecuteAsync<int>("getconnectioncount", 0);
        }

        public Task<JsonRpcResponse<NetTotalsResponse>> GetNetTotalsAsync()
        {
            return this.ExecuteAsync<NetTotalsResponse>("getnettotals", 0);
        }

        public Task<JsonRpcResponse<NetworkInfoResponse>> GetNetworkInfoAsync()
        {
            return this.ExecuteAsync<NetworkInfoResponse>("getnetworkinfo", 0);
        }

        public Task<JsonRpcResponse<decimal>> GetUnconfirmedBalanceAsync()
        {
            return this.ExecuteAsync<decimal>("getunconfirmedbalance", 0);
        }

        public Task<JsonRpcResponse<WalletInfoResponse>> GetWalletInfoAsync()
        {
            return this.ExecuteAsync<WalletInfoResponse>("getwalletinfo", 0);
        }

        public Task<JsonRpcResponse<decimal>> EstimateFeeAsync(int numBlocks)
        {
            return this.ExecuteAsync<decimal>("estimatefee", 0, numBlocks);
        }

        public Task<JsonRpcResponse<decimal>> EstimatePriorityAsync(int numBlocks)
        {
            return this.ExecuteAsync<decimal>("estimatepriority", 0, numBlocks);
        }

        public Task<JsonRpcResponse<AddressResponse>> ValidateAddressAsync(string address)
        {
            return this.ExecuteAsync<AddressResponse>("validateaddress", 0, address);
        }

        public Task<JsonRpcResponse<List<TransactionResponse>>> ListTransactionsAsync(string account = null, int count = 10, int skip = 0, bool watchOnly = false)
        {
            return this.ExecuteAsync<List<TransactionResponse>>("listtransactions", 0, account ?? string.Empty, count, skip, watchOnly);
        }

        public Task<JsonRpcResponse<string>> SendFromAsync(string fromAccount, string toAddress, decimal amount, int confirmations = 1, string comment = null, string commentTo = null)
        {
            return this.ExecuteAsync<string>("sendfrom", 0, fromAccount ?? string.Empty, toAddress, amount, confirmations, comment ?? string.Empty, commentTo ?? string.Empty);
        }

        public Task<JsonRpcResponse<TxOutResponse>> GetTxOutAsync(string txId, int vout = 0, bool unconfirmed = false)
        {
            return this.ExecuteAsync<TxOutResponse>("gettxout", 0, txId, vout, unconfirmed);
        }

        public Task<JsonRpcResponse<TxOutSetInfoResponse>> GetTxOutSetInfoAsync()
        {
            return this.ExecuteAsync<TxOutSetInfoResponse>("gettxoutsetinfo", 0);
        }

        public Task<JsonRpcResponse<decimal>> GetBalanceAsync(string account = null, int confirmations = 1, bool watchOnly = false)
        {
            return this.ExecuteAsync<decimal>("getbalance", 0, account ?? "*", confirmations, watchOnly);
        }

        public Task<JsonRpcResponse<bool>> VerifyChainAsync(CheckBlockType type = CheckBlockType.TestEachBlockUndo, int numBlocks = 0)
        {
            return this.ExecuteAsync<bool>("verifychain", 0, (int)type, numBlocks);
        }

        // not implemented -- contact us with specific implementation requirements and we'll implement this...
        public Task<JsonRpcResponse<string>> CreateRawTransactionAync()
        {
            throw new NotImplementedException("This operation has not been implemented.");
        }

        // not implemented -- contact us with specific implementation requirements and we'll implement this...
        public Task<JsonRpcResponse<string>> SendRawTransactionAsync()
        {
            throw new NotImplementedException("This operation has not been implemented.");
        }

        // not implemented -- contact us with specific implementation requirements and we'll implement this...
        public Task<JsonRpcResponse<string>> SignRawTransactionAsync()
        {
            throw new NotImplementedException("This operation has not been implemented.");
        }

        public Task<JsonRpcResponse<bool>> PrioritiseTransactionAsync(string txId, decimal priority, int feeSatoshis)
        {
            return this.ExecuteAsync<bool>("prioritisetransaction", 0, txId, priority, feeSatoshis);
        }

        public Task<JsonRpcResponse<long>> GetNetworkHashPsAsync(int blocks = 120, int height = -1)
        {
            return this.ExecuteAsync<long>("getnetworkhashps", 0, blocks, height);
        }

        public Task<JsonRpcResponse<bool>> SetTxFeeAsync(decimal fee)
        {
            return this.ExecuteAsync<bool>("settxfee", 0, fee);
        }

        public Task<JsonRpcResponse<List<AssetBalanceResponse>>> GetTotalBalancesAsync(int confirmations = 1, bool watchOnly = false, bool locked = false)
        {
            return this.ExecuteAsync<List<AssetBalanceResponse>>("gettotalbalances", 0, confirmations, watchOnly, locked);
        }

        public Task<JsonRpcResponse<string>> KeypoolRefillAsync(int size = 0)
        {
            return this.ExecuteAsync<string>("keypoolrefill", 0, size);
        }

        public Task<JsonRpcResponse<object>> GetBlockTemplateAsync()
        {
            return this.ExecuteAsync<object>("getblocktemplate", 0);
        }

        public Task<JsonRpcResponse<Dictionary<string, decimal>>> ListAccountsAsync()
        {
            return this.ExecuteAsync<Dictionary<string, decimal>>("listaccounts", 0);
        }

        public Task<JsonRpcResponse<List<List<List<object>>>>> ListAddressGroupingsAsync()
        {
            return this.ExecuteAsync<List<List<List<object>>>>("listaddressgroupings", 0);
        }

        public Task<JsonRpcResponse<decimal>> GetReceivedByAccountAsync(string account = null, int confirmations = 1)
        {
            return this.ExecuteAsync<decimal>("getreceivedbyaccount", 0, account ?? string.Empty, confirmations);
        }

        public Task<JsonRpcResponse<List<ReceivedResponse>>> ListReceivedByAddressAsync(int confirmations = 1, bool empty = false, bool watchOnly = false)
        {
            return this.ExecuteAsync<List<ReceivedResponse>>("listreceivedbyaddress", 0, confirmations);
        }

        public Task<JsonRpcResponse<List<ReceivedResponse>>> ListReceivedByAccountAsync(int confirmations = 1, bool empty = false, bool watchOnly = false)
        {
            return this.ExecuteAsync<List<ReceivedResponse>>("listreceivedbyaccount", 0, confirmations);
        }

        public Task<JsonRpcResponse<decimal>> GetReceivedByAddressAsync(string address, int confirmations = 1)
        {
            return this.ExecuteAsync<decimal>("getreceivedbyaddress", 0, address, confirmations);
        }

        public Task<JsonRpcResponse<GetTransactionResponse>> GetTransactionAsync(string txId, bool watchOnly = false)
        {
            return this.ExecuteAsync<GetTransactionResponse>("gettransaction", 0, txId, watchOnly);
        }

        public Task<JsonRpcResponse<string>> GetRawChangeAddressAsync()
        {
            return this.ExecuteAsync<string>("getrawchangeaddress", 0);
        }

        public Task<JsonRpcResponse<string>> ImportAddressAsync(string address, string account = null, bool rescan = true)
        {
            return this.ExecuteAsync<string>("importaddress", 0, address, account ?? string.Empty, rescan);
        }

        public Task<JsonRpcResponse<string>> ImportPrivKey(string key, string account = null, bool rescan = true)
        {
            return this.ExecuteAsync<string>("importprivkey", 0, key, account ?? string.Empty, rescan);
        }

        public Task<JsonRpcResponse<string>> GetAccountAsync(string address)
        {
            return this.ExecuteAsync<string>("getaccount", 0, address);
        }

        public Task<JsonRpcResponse<string>> SetAccountAsync(string address, string account)
        {
            return this.ExecuteAsync<string>("setaccount", 0, address, account);
        }

        public Task<JsonRpcResponse<string>> GetAccountAddressAsync(string account)
        {
            return this.ExecuteAsync<string>("getaccountaddress", 0, account ?? string.Empty);
        }

        public Task<JsonRpcResponse<List<AssetBalanceResponse>>> GetAssetBalancesAsync(string account = null, int confirmations = 1, bool watchOnly = false, bool includeLocked = false)
        {
            return this.ExecuteAsync<List<AssetBalanceResponse>>("getassetbalances", 0, account ?? string.Empty, confirmations, watchOnly, includeLocked);
        }

        public Task<JsonRpcResponse<List<AssetBalanceResponse>>> GetAddressBalancesAsync(string address, int confirmations = 1, bool includeLocked = false)
        {
            return this.ExecuteAsync<List<AssetBalanceResponse>>("getaddressbalances", 0, address, confirmations, includeLocked);
        }

        public Task<JsonRpcResponse<List<string>>> GetAddressesByAccountAsync(string account)
        {
            return this.ExecuteAsync<List<string>>("getaddressesbyaccount", 0, account ?? string.Empty);
        }

        public Task<JsonRpcResponse<ListSinceLastBlockResponse>> ListSinceBlockAsync(string hash, int confirmations = 1, bool watchOnly = false)
        {
            return this.ExecuteAsync<ListSinceLastBlockResponse>("listsinceblock", 0, hash, confirmations, watchOnly);
        }

        public Task<JsonRpcResponse<List<UnspentResponse>>> ListUnspentAsync(int minConf = 1, int maxConf = 999999, IEnumerable<string> addresses = null)
        {
            return this.ExecuteAsync<List<UnspentResponse>>("listunspent", 0, minConf, maxConf);
        }

        public Task<JsonRpcResponse<List<string>>> ListLockUnspentAsync()
        {
            return this.ExecuteAsync<List<string>>("listlockunspent", 0);
        }

        public Task<JsonRpcResponse<List<string>>> GetAddressesAsync()
        {
            return this.ExecuteAsync<List<string>>("getaddresses", 0);
        }

        public Task<JsonRpcResponse<List<AddressResponse>>> GetAddressesVerboseAsync()
        {
            return this.ExecuteAsync<List<AddressResponse>>("getaddresses", 0, true);
        }

        public Task<JsonRpcResponse<string>> PingAsync()
        {
            return this.ExecuteAsync<string>("ping", 0);
        }

        public Task<JsonRpcResponse<string>> BackupWalletAsync(string path)
        {
            return this.ExecuteAsync<string>("backupwallet", 0, path);
        }

        public Task<JsonRpcResponse<string>> DumpWalletAsync(string path)
        {
            return this.ExecuteAsync<string>("dumpwallet", 0, path);
        }

        public Task<JsonRpcResponse<string>> ImportWallet(string path)
        {
            return this.ExecuteAsync<string>("importwallet", 0, path);
        }

        public Task<JsonRpcResponse<string>> EncryptWalletAsync(string passphrase)
        {
            return this.ExecuteAsync<string>("encryptwallet", 0, passphrase);
        }

        public Task<JsonRpcResponse<string>> DumpPrivKeyAsync(string address)
        {
            return this.ExecuteAsync<string>("dumpprivkey", 0, address);
        }

        public Task<JsonRpcResponse<string>> AddNodeAsync(string address, AddNodeCommand command)
        {
            return this.ExecuteAsync<string>("addnode", 0, address, command.ToString().ToLower());
        }

        public Task<JsonRpcResponse<List<string>>> GetAddedNodeInfoAsync()
        {
            return this.ExecuteAsync<List<string>>("getaddednodeinfo", 0, false);
        }

        public Task<JsonRpcResponse<List<string>>> GetAddedNodeInfoAsync(string node)
        {
            return this.ExecuteAsync<List<string>>("getaddednodeinfo", 0, false, node);
        }

        public Task<JsonRpcResponse<List<string>>> GetAddedNodeInfoDetailsAsync()
        {
            return this.ExecuteAsync<List<string>>("getaddednodeinfo", 0, true);
        }

        public Task<JsonRpcResponse<List<string>>> GetAddedNodeInfoDetailsAsync(string node)
        {
            return this.ExecuteAsync<List<string>>("getaddednodeinfo", 0, true, node);
        }

        public Task<JsonRpcResponse<MultiSigResponse>> CreateMultiSigAsync(int numRequired, IEnumerable<string> addresses)
        {
            return this.ExecuteAsync<MultiSigResponse>("createmultisig", 0, numRequired, addresses);
        }

        public Task<JsonRpcResponse<string>> SubmitBlockAsync(byte[] bs, object args = null)
        {
            if (args != null)
                return this.ExecuteAsync<string>("submitblock", 0, bs, args);
            else
                return this.ExecuteAsync<string>("submitblock", 0, bs);
        }

        public Task<JsonRpcResponse<ScriptResponse>> DecodeScriptAsync(string decodeScript)
        {
            return this.ExecuteAsync<ScriptResponse>("decodescript", 0, decodeScript);
        }

        public Task<JsonRpcResponse<string>> AddMultiSigAddressAsync(int numRequired, IEnumerable<string> addresses, string account = null)
        {
            return this.ExecuteAsync<string>("addmultisigaddress", 0, numRequired, addresses, account ?? string.Empty);
        }

        public Task<JsonRpcResponse<string>> HelpAsync()
        {
            return this.ExecuteAsync<string>("help", 0);
        }

        public Task<JsonRpcResponse<string>> HelpAsync(string command)
        {
            return this.ExecuteAsync<string>("help", 0, command);
        }

        public Task<JsonRpcResponse<object>> StopAsync()
        {
            return this.ExecuteAsync<object>("stop", 0);
        }

        public static string FormatHex(byte[] bs)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var b in bs)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }

        private class NullResponse
        {
        }

        private async Task<JsonRpcResponse<T>> ExecuteAsync<T>(string method, int id, params object[] args)
        {
            var ps = new JsonRpcRequest()
            {
                Method = method,
                Params = args,
                ChainName = this.ChainName,
                ChainKey = this.ChainKey,
                Id = id
            };

            // defer...
            this.OnExecuting(new EventArgs<JsonRpcRequest>(ps));

            var jsonOut = JsonConvert.SerializeObject(ps.Values);
            var url = this.ServiceUrl;
            try
            {
                var request = WebRequest.CreateHttp(url);
                request.Credentials = this.GetCredentials();
                request.Method = "POST";

                var bs = Encoding.UTF8.GetBytes(jsonOut);
                using (var stream = await request.GetRequestStreamAsync())
                    stream.Write(bs, 0, bs.Length);

                // get the response...
                var response = await request.GetResponseAsync();
                string jsonIn = null;
                using (var stream = ((HttpWebResponse)response).GetResponseStream())
                    jsonIn = await new StreamReader(stream).ReadToEndAsync();

                // return...
                JsonRpcResponse<T> theResult = null;
                try
                {
                    theResult = JsonConvert.DeserializeObject<JsonRpcResponse<T>>(jsonIn);
                }
                catch (Exception jsonEx)
                {
                    throw new InvalidOperationException("Failed to deserialize JSON.\r\nJSON: " + jsonIn, jsonEx);
                }
                theResult.RawJson = jsonIn;
                return theResult;
            }
            catch (Exception ex)
            {
                var walk = ex;
                string errorData = null;
                while (walk != null)
                {
                    if (walk is WebException)
                    {
                        var webEx = (WebException)walk;
                        if (webEx.Response != null)
                        {
                            using (var stream = webEx.Response.GetResponseStream())
                                errorData = new StreamReader(stream).ReadToEnd();
                        }

                        break;
                    }

                    walk = walk.InnerException;
                }

                throw new InvalidOperationException(string.Format("Failed to issue JSON-RPC request.\r\nData: {0}\r\nURL: {1}\r\nJSON: {2}", errorData, url, jsonOut), ex);
            }
        }

        protected virtual void OnExecuting(EventArgs<JsonRpcRequest> e)
        {
            if (this.Executing != null)
                this.Executing(this, e);
        }

        private string ServiceUrl
        {
            get
            {
                var protocol = "https";
                if (!(this.UseSsl))
                    protocol = "http";
                return string.Format("{0}://{1}:{2}/", protocol, this.Hostname, this.Port);
            }
        }

        private ICredentials GetCredentials()
        {
            if (this.HasCredentials)
                return new NetworkCredential(this.Username, this.Password);
            else
                return null;
        }

        public bool HasCredentials
        {
            get
            {
                return !(string.IsNullOrEmpty(this.Username));
            }
        }
    }
}
