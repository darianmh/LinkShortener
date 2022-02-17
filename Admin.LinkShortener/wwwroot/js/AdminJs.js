//admin image file upload
var targetInputName = '';
var page = 1;
var count = 10;
function LoadAdminImage(targetName) {
  page = 1;
  targetInputName = targetName;
  $("#ModalImageContainer").html('');
  LoadImages();
}
function LoadImages() {
  $.ajax({
    url: "/AjaxService/LoadImages",
    data: { page, count },
    success: function (result) {
      $("#ModalImageContainer").append(result);
      $("#exampleModalFull").modal('show');
      page++;
    }
  });
}

function SetSelectedImage(path) {
  $("#" + targetInputName).val(path);
  $("#exampleModalFull").modal('hide');
  var image = '<div class="col-2  mb-4"><img class="w-100 img-fluid rounded" src="' + path + '" ></div>';
  $("#" + targetInputName + "Pre").html(image);
}

//admin save and continue
function Save() {
  $("form").append('<input type="hidden" name="SaveAndContinue" value="1"/>');
  $("form").submit();
}