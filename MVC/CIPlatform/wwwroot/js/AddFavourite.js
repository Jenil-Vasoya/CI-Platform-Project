function Add(MissionId) {
    debugger
    $.ajax({

        url: "/Home/AddToFavourite",
        method: "POST",

        data:
        {
            MissionId: MissionId

        },
        success: function (data) {

            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };

            if (data == true) {
                console.log('addtofav output:'.data);

                $('#favButtonText').text('Remove from Favourite');
                $("#addToFav_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
                $("#addToFavList_" + MissionId).removeClass('bi-heart text-white').addClass('bi-heart-fill  text-danger');
                toastr.success('Added favourite successfully');
            }
            else {

                $('#favButtonText').text('Add to Favourite');
                $("#addToFav_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
                $("#addToFavList_" + MissionId).removeClass('bi-heart-fill').addClass('bi-heart text-white');
                toastr.error('Remove favourite successfully');
            }
        }
    })
};
