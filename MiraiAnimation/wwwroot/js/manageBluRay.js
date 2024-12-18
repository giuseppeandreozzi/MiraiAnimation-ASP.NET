function loadBD(){
	if ($("#editBD option:checked").val() == ""){
		$("#cofanetto").hide();
		return;
	}

	var formData = new FormData();
	formData.append("id", $("#editBD option:checked").val());

	$.ajax({
		url:"/infoBluRay",
		type: "POST",
		data: formData,
		contentType: false,
		processData: false,
		success: function(result){
			$("#editBD textarea[name=descrizione]").val(result.descrizione);
			$("#editBD input[name=prezzo]").val(result.prezzo);
			
			$("#cofanetto").show();
		}
	});
}

function checkForm(form){
	var contenuto = form.descrizione;
	var prezzo = form.prezzo;
	
	var patternContenuto = /^.{1,100}$/g;
	var patternPrezzo = /^\d{1,}$/g;
	
	if (!patternContenuto.test(contenuto.value)){
		$("#" + form.id + " .errorContenuto").html("Descrizione non valida<br/>");
		contenuto.focus();
		return false;
	}
	
	if (!patternPrezzo.test(prezzo.value) || prezzo.value < 1){
		$("#" + form.id + " .errorPrezzo").html("Inserire un prezzo valido (maggiore di 0) <br/>");
		prezzo.focus();
		return false;
	}
	
	return true;
}