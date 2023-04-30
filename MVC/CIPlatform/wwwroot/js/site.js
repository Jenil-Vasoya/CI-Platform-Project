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

function Add(MissionId,btn) {
    debugger
    $.ajax({

        url: "/Home/AddToFavourite",
        method: "POST",

        data:
        {
            MissionId: MissionId

        },
        success: function (data) {

            if (data == true) {

                debugger
                if (btn == 'btnAddToFavVolun_' || btn == 'btnAddToFavGrid_') {

                    $('#favButtonText').text(' Remove from Favourite');
                    $("#addToFavGrid_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
                    $("#addToFavVolun_" + MissionId).removeClass('bi-heart text-black').addClass('bi-heart-fill  text-danger');
                    $("#addToFavList_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
                    toastr.success('Added to Favourite');
                }
                else if (btn == 'btnaddToFav_') {
                    //$("#addToFav_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
                    $("#addToFavGrid_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
                    $("#addToFavList_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
                    toastr.success('Added to Favourite');
                }
               
            }
            else {

                if (btn == 'btnAddToFavVolun_' || btn == 'btnAddToFavGrid_') {

                    $('#favButtonText').text(' Add to Favourite');
                    $("#addToFavGrid_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
                    $("#addToFavVolun_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-black');
                    $("#addToFavList_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
                    toastr.error('Remove from Favourite');
                }
                else if (btn == 'btnaddToFav_') {
                    //$("#addToFav_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
                    $("#addToFavGrid_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
                    $("#addToFavList_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
                    toastr.error('Remove from Favourite');
                }

               
            }
        }
    })
};


function CheckUser(userId, missionId) {
    debugger
    $.ajax({

        url: "/Home/CheckUser",
        method: "POST",

        data:
        {
            userId: userId,
            missionId: missionId

        },
        success: function (data) {

            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };

            if (data == true) {
                
                $('#exampleModal_' + missionId).modal('hide');
                $('#exampleModal1_' + missionId).modal('hide');
                $('#exampleModal').modal('hide');
                $('#' + userId).prop('checked', false);
                toastr.error('You already invite this user');
            }
           
        }
    })
};


//function Add(MissionId, btn) {
//    debugger
//    $.ajax({

//        url: "/Home/AddToFavourite",
//        method: "POST",

//        data:
//        {
//            MissionId: MissionId

//        },
//        success: function (data) {
//            console.log(data)
//            debugger
//            toastr.options = {
//                "closeButton": true,
//                "progressBar": true
//            };
//            console.log(MissionId)
//            console.log(btn)
//            if (data == true) {
//                debugger
//                if (btn == 'btnAddToFavVolun_') {

//                    $('#favButtonText').text('Remove from Favourite');
//                    $("#addToFavVolun_" + MissionId).removeClass('bi-heart text-black').addClass('bi-heart-fill  text-danger');
//                    toastr.success('Added favourite successfully');
//                }
//                else if (btn == 'btnaddToFav_') {
//                    $("#addToFav_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
//                    toastr.success('Added favourite successfully');
//                }
//            }
//            else {
//                if (btn == 'btnAddToFavVolun_') {

//                    $('#favButtonText').text('Add to Favourite');
//                    $("#addToFavVolun_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-black');
//                    toastr.error('Remove favourite successfully');
//                }
//                else if (btn == 'btnaddToFav_') {
//                    $("#addToFav_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
//                    toastr.error('Remove favourite successfully');
//                }
//                @* $('#favButtonText').text('Add to Favourite');
//                $("#addToFav_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
//                $("#addToFavVolun_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-black');
//                toastr.error('Remove favourite successfully');*@
//                }
//        }
//    })
//}


//function Add(MissionId) {
//    debugger
//    $.ajax({

//        url: "/Home/AddToFavourite",
//        method: "POST",

//        data:
//        {
//            MissionId: MissionId

//        },
//        success: function (data) {

//            toastr.options = {
//                "closeButton": true,
//                "progressBar": true
//            };

//            if (data == true) {
//                console.log('addtofav output:'.data);

//                $('#favButtonText').text('Remove from Favourite');
//                $("#addToFav_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
//                $("#addToFavVolun_" + MissionId).removeClass('bi-heart text-black').addClass('bi-heart-fill  text-danger');
//                $("#addToFavList_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
//                toastr.success('Added favourite successfully');
//            }
//            else {

//                $('#favButtonText').text('Add to Favourite');
//                $("#addToFav_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
//                $("#addToFavVolun_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-black');
//                $("#addToFavList_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
//                toastr.error('Remove favourite successfully');
//            }
//        }
//    })
//};




//function Add(MissionId) {
//    debugger
//    $.ajax({

//        url: "/Home/AddToFavourite",
//        method: "POST",

//        data:
//        {
//            MissionId: MissionId

//        },
//        success: function (data) {

//            toastr.options = {
//                "closeButton": true,
//                "progressBar": true
//            };

//            if (data == true) {

//                $('#favButtonText').text('Remove from Favourite');
//                $("#addToFav_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
//                $("#addToFavVolun_" + MissionId).removeClass('bi-heart text-black').addClass('bi-heart-fill  text-danger');
//                toastr.success('Added favourite successfully');
//            }
//            else {

//                $('#favButtonText').text('Add to Favourite');
//                $("#addToFav_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
//                $("#addToFavVolun_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-black');
//                toastr.error('Remove favourite successfully');
//            }
//        }
//    })
//}