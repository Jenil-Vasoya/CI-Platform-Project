// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//function preventBack() { window.history.forward(); }
//setTimeout("preventBack()", 0);
//window.onunload = function () { null }


$(document).ready(function (a) {
    //---Convet-Grid-List---
    localStorage.setItem("lastVisible", "grid");
    var windowWidth = $(window).width();
    // Check if the window width is less than 1440px
    if (windowWidth < 991) {
        // Hide the list view and show the gird view
        $('.listItem').hide();
        $('.gridItem').show();
    }
    $(window).resize(function (e) {
        // Get the new window width
        var newWindowWidth = $(window).width();

        // Check if the window width is less than 1440px
        if (newWindowWidth < 991) {
            // Hide the list view and show the gird view
            $('.listItem').hide();
            $('.gridItem').show();
            $("#list").hide();
            $("#grid").hide();
        }
        else {
            $("#list").show();
            $("#grid").show();
            // Show the list view and hide the gird view
            if (localStorage.getItem("lastVisible") == "list") {
                $('.gridItem').hide();
                $('.listItem').show();
            }
        }
    });


});