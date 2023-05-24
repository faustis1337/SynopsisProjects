// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

var ViewModel = function() {
    console.log("View Model started")
    
    let me =  this;
    
    me.userQuery = ko.observable();
    me.returnedQuery = ko.observableArray();
    me.queryMatches = ko.observable();
    me.queryTime = ko.observable();
    me.username = ko.observable();
    me.password = ko.observable();
    
    me.init = function (){
        document.getElementById('search-button').disabled = true;
        const apiKey = "b40958b8-3bd8-4b6b-bf1c-3d3548b724cd/BsMebU8H8nEEiVlBkmk2txLjGmBFmnqXhWss8bAF";
        
        $.ajax({
            url: "http://localhost:8085/features",
            type: "GET",
            data: {apiKey: apiKey, contextSha: 0},
            dataType: "json",
            success: function (data) {
                const loginEnabled = data[0].features[0].value;
                if(!loginEnabled) {
                    document.getElementById('login-container').style.display = 'none';
                    document.getElementById('search-button').disabled = false;
                }
            },
            error: function () {
                document.getElementById('login-container').style.display = 'none';
                document.getElementById('search-button').disabled = false;
                console.log("Feature hub failed therefore feature wont be shown");
            }
        })
    }
    
    me.search = function (){
        console.log("was updated1");
        $.ajax({
            url: "http://localhost:9020/LoadBalancer?terms=" + me.userQuery() + "&numberOfResults=10",
            //url: "http://myapp-service.default.svc.cluster.local:80/LoadBalancer?terms=" + me.userQuery() + "&numberOfResults=10",
            success: function (data) {
                me.queryMatches(data.documents.length);
                me.queryTime(data.elapsedMilliseconds);
                me.returnedQuery.removeAll();
                data.documents.forEach( function (hit) {
                    me.returnedQuery.push(hit);
                    console.log(hit);
                });
            }
            });
    }
    
    me.login = function (){
        $.ajax({
            url: "http://localhost:9025/UserLoadBalancer",

            data: {username: me.username, password: me.password},       
            success: function () {
                console.log("LOGIN SUCCESSFUL");
                document.getElementById('search-button').disabled = false;
            }
        })
    }
}
// Create an instance of the view model
var viewModelInstance = new ViewModel();

// Bind the view model to the HTML view
ko.applyBindings(viewModelInstance);

// Call the init function
viewModelInstance.init();
