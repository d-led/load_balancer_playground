<Query Kind="Statements">
  <NuGetReference>RestSharp</NuGetReference>
  <Namespace>RestSharp</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

var client = new ThreadLocal<RestClient>(()=>new RestClient("http://localhost:8000"));
var request = new RestRequest("/",Method.GET);
request.Timeout=100;
var load = Enumerable
	.Range(1, 10000)
	.AsParallel()
	.Select(_ => client.Value.Execute(request))
	.ToArray()
;

load.Last().Content.Dump();

$"Errors: {load.Where(r => r.StatusCode != HttpStatusCode.OK).Count()}".Dump();
