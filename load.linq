<Query Kind="Statements">
  <NuGetReference>RestSharp</NuGetReference>
  <Namespace>RestSharp</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

var client = new ThreadLocal<RestClient>(()=>new RestClient("http://localhost:8000"));
var request = new RestRequest("/",Method.GET);
var load = Enumerable
	.Range(1, 10000)
	.AsParallel()
	.Select(_ => client.Value.Execute(request))
	.ToArray()
;

load.Last().Content.Dump();

load.GroupBy(r => r.Content).Select(g => new
{
	Port=g.Key,
	Count=g.Count()
}).Distinct().Dump();

$"Errors: {load.Where(r => r.StatusCode != HttpStatusCode.OK).Count()}".Dump();
