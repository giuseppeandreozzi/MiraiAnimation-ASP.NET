function checkStar(id){
	var subId = id.substring(1);

	$("input[name=voto]").val(id[0]);
	for (var i = 1; i <= 5; i++){
		if (i <= id[0])
			$("#" + i + subId).css("color", "orange");
		else
			$("#" + i + subId).css("color", "black");
	}
}

function checkForm(){
	var comment = reviewForm.comment;
	var spanError = document.getElementById("errorComment");
	
	var patternComment = /^.{1,255}$/g;
	
	if(!patternComment.test(comment.value)){
		spanError.innerHTML = "Il commento non pu&ograve; essere vuoto o avere pi&ugrave; di 255 caratteri";
		comment.focus();
		return false;
	}
	
	return true;
}