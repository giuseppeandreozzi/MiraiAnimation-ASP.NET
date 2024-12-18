function loadStaff() {
	if ($("option:checked").val() == ""){
		$("#staff").hide();
		return;
	}

	var formData = new FormData();
	formData.append("id", $("option:checked").val())
			
	$.ajax({
		url:"/infoStaff",
		type: "POST",
		data: formData,
		contentType: false,
		processData: false,
		success: function(result){
			$("#editStaff input[name=anniServizio]").val(result.anniServizio);
			$("#editStaff input[name=nome]").val(result.nome);
			$("#editStaff input[name=cognome]").val(result.cognome);
			if (result.ruolo == "Direttore") {
				$("#editStaff input[value=Direttore]").click();
			} else if (result.ruolo == "Staff tecnico") {
				$("#editStaff input[value='Staff tecnico']").click();
			} else if (result.ruolo == "Regista") {
				$("#editStaff input[value=Regista]").click();
			}
			
			$("#staff").show();
				     
		}
	});
}

function checkEditForm(){
	var anniServizio = editStaff.anniServizio;
	var ruolo = $("#editStaff input[name=ruolo]:checked").val();
	
	var patternAnni = /^\d{1,}$/g;
	
	if (!patternAnni.test(anniServizio.value) || anniServizio.value < 0){
		$("#errorAnniEdit").html("Inserire un anno di servizio valido<br/>");
		anniServizio.focus();
		return false;
	}
	
	if (ruolo != "Staff tecnico" && ruolo != "Direttore" && ruolo != "Regista"){
		$("#errorRuoloEdit").html("Ruolo non valido<br/>");
		return false;
	}
	
	return true;
}

function checkInsertForm(){
	var codiceFiscale = insertStaff.codiceFiscale;
	var nome = insertStaff.nome;
	var cognome = insertStaff.cognome;
	var anniServizio = insertStaff.anniServizio;
	var ruolo = $("#insertStaff input[name=ruolo]:checked").val();

	var patternCF = /^[A-Z0-9]{16}$/g;
	var patternNome = /^[a-zA-Z]{3,20}$/g;
	var patternCognome = /^[a-zA-Z]{3,20}$/g;
	var patternAnni = /^\d{1,}$/g;
	
	if (!patternCF.test(codiceFiscale.value)){
		$("#errorCF").html("Inserire un codice fiscale valido<br/>");
		codiceFiscale.focus();
		return false;
	}
	
	if (!patternNome.test(nome.value)){
		$("#errorNome").html("Il nome deve essere lungo tra 3 e 20 caratteri e può contenere solo lettere<br/>");
		nome.focus();
		return false;
	}
	
	if (!patternCognome.test(cognome.value)){
		$("#errorCognome").html("Il cognome deve essere lungo tra 3 e 20 caratteri e può contenere solo lettere<br/>");
		cognome.focus();
		return false;
	}
	
	if (!patternAnni.test(anniServizio.value) || anniServizio.value < 0){
		$("#errorAnniInsert").html("Inserire un anno di servizio valido<br/>");
		anniServizio.focus();
		return false;
	}
	
	if (ruolo != "Staff tecnico" && ruolo != "Direttore" && ruolo != "Regista"){
		$("#errorRuoloInsert").html("Ruolo non valido<br/>");
		return false;
	}
	
	return true;
}