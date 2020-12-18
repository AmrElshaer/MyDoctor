
    "use strict";
        let addLike = (postId) => {
        $.ajax({
            type: "GET",
            url: `/Post/AddLike?postId=${postId}`,
            success: (data) => {
                updateLikeDisLike(data);
            }
        });
        };
        let addDisLike=(postId) =>{
        $.ajax({
            type: "GET",
            url: `/Post/AddDisLike?postId=${postId}`,
            success: (data) => {
                updateLikeDisLike(data);
            }
        });
        };
        let updateLikeDisLike = (data) => {
        document.getElementById('LikeNumber').innerText = data.likeNumber;
            document.getElementById('DisLikeNumber').innerText = data.disLikeNumber;
        };
