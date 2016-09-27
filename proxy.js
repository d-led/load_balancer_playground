//https://github.com/FriendCode/loadfire
var loadfire = require('loadfire');

var PORTS = [7001, 7002, 7003];

var WORKERS = PORTS.map(function(x) {
    return {
        host: 'localhost',
        port: x
    };
});

var CONFIG = {
    'resources': [
        {
            selector: loadfire.selectors.host('localhost:8000'),
            backends: WORKERS,
            balancer: function(backends, req, cb) {
                return loadfire.balancers.roundrobin(backends, req, cb);
            }
        }
    ],
    port: 8000
};

function main() {
    var loadServer = loadfire.createServer(CONFIG);
    loadServer.run();
}

main();
