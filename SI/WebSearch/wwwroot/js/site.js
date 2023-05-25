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
    
    const endpoint = "http://localhost:30016"; // http://localhost:9020
    
    me.search = function (){
        $.ajax({
            url: endpoint + "/LoadBalancer?terms=" + me.userQuery() + "&numberOfResults=10",
            success: function (data) {
                me.queryMatches(data.documents.length);
                me.queryTime(data.elapsedMlliseconds);
                me.returnedQuery.removeAll();
                data.documents.forEach( function (hit) {
                    me.returnedQuery.push(hit);
                    console.log(hit);
                });
            }
            });
    }
}
// Create an instance of the view model
var viewModelInstance = new ViewModel();

// Bind the view model to the HTML view
ko.applyBindings(viewModelInstance);
