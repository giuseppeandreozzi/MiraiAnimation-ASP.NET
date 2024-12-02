	
function insertError(){
	var errorType = $("#errorType").val();

	if(errorType == "sign"){
		$("span#errorSign").html($("#error").val());
		$("span#errorSign").show();
		$('#signUpModal').modal('show');
	}
	else if(errorType == "log"){
		$("span#errorLog").html($("#error").val());
		$("span#errorLog").show();
		$('.dropdown-toggle').dropdown('toggle');
	}
}

$(".animation").click(function(){
    $(".animation").removeClass("mx-auto");
  });

$(".animation").dblclick(function(){
    $(".animation").addClass("mx-auto");
  });