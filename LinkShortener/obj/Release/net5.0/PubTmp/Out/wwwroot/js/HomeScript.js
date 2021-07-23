$(document).ready(function () {
  $("#search").focus(function () {
    $(".search-box").addClass("border-searching");
    $(".search-icon").addClass("si-rotate");
  });
  $("#search").blur(function () {
    $(".search-box").removeClass("border-searching");
    $(".search-icon").removeClass("si-rotate");
  });
  $("#search").keyup(function () {
    if ($(this).val().length > 0) {
      $(".go-icon").addClass("go-in");
    }
    else {
      $(".go-icon").removeClass("go-in");
    }
  });
  $(".go-icon").click(function () {
    $(".search-form").submit();
  });
});

var shortLink;
//link generation
$('.search-form').submit(function (e) {
  e.preventDefault();
  getLink();
})
function getLink() {
  startLoading();
  var userLink = $('#search').val();
  if (userLink === null || userLink === '') return MyAlert('لینک معتبر نمی باشد.');
  userLink = encodeURIComponent(userLink);
  var url = '/api/links/' + userLink;
  $.ajax({
    url: url,
    success: function (data) {
      console.log(data);
      if (data.ok) {
        shortLink = "https://ls1.ir/" + data.data.shortLink;
        $('#shortLinkText').text(shortLink);
        $('.shortLink').removeClass('hidden');
      } else {
        MyAlert(data.error);
      }
      stopLoading();
    },
    error: function (data) {
      stopLoading();
    }
  });
}


//copy link
$('.shortLink').click(function () {

  $('#Short_Link').val(shortLink);

  /* Get the text field */
  var copyText = document.getElementById("Short_Link");

  /* Select the text field */
  copyText.select();

  /* Copy the text inside the text field */
  document.execCommand("copy");

  /* Alert the copied text */
  MyAlert("لینک شما کپی شد: " + copyText.value);
})

//function custom alert
function MyAlert(text) {
  alert(text);
}
//modal
// Get the modal
var modal = document.getElementById("myModal");


// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal 
function openModal() {
  modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
  modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
}
//end modal

//loading
function startLoading() {
  $('.loader').fadeIn(350);
}
function stopLoading() {
  $('.loader').fadeOut();
}