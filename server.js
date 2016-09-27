var http = require('http');

const port = process.argv[2];

var httpServer = http.createServer(function(req, res) {
    res.writeHead(200, {'Content-Type': 'text/plain'});
    res.end("v1/" + port);
});

function main() {
    console.log(port);
    httpServer.listen(port);
}

main();
