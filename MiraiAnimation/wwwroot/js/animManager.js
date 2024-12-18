function loadAnim(){
	if ($("option:checked").val() == ""){
		$("div#anim").hide();
		return;
	}
	var formData = new FormData();
	formData.append("codiceAnimazione", $("option:checked").val());
	var jsonObj = {};
	jsonObj.codiceAnimazione = $("option:checked").val();

	$.ajax({
		url:"/infoAnimazione",
		type: "POST",
		data: formData,
        contentType: false,
        processData: false,
		success: function (result) {
			$("#anim input[name=id]").val(result.id);
			$("#anim input[name=titolo]").val(result.anim.titolo);
			$("#anim input[name=genere]").val(result.anim.genere);
			$("#anim input[name=dataUscita]").val(result.anim.dataUscita);
			if (result.anim.tipo == "serie")
				$("#anim input[value=serie]").click();
			else
				$("#anim input[value=film]").click();
				    
			$("#anim").show();
				     
		}
	});
} //fine loadAnim
		
		
function updateInfo(element){ //element contiene riferimento al radio button che viene selezionato
	var parentID = $(element).parent().attr("id");

	if ($("#" + parentID + " input[name=tipo]:checked").val() == "serie"){
		$("#" + parentID + " div.serie").show();
	} else {
		$("#" + parentID + " div.serie").hide();
	}
}

function updateInfoDeleteStaff(element){ //element contiene riferimento al radio button che viene selezionato
	var parentID = $(element).parent().attr("id");

	$("#" + parentID + " select").hide();

	$("#" + parentID + " select#" + $(element).attr("id")).show();
}

function checkInsertEditForm(form){
	var nome = form.nome;
	var genere = form.genere;
	var tipo = form.tipo;
	var episodi;
	var uscita;
	
	var patternNome = /^.{2,20}$/g;
	var patternGenere = /^\w{2,20}$/g;
	var patternEpisodi = /^\d+$/g;
	var patternUscita = /^\d{4}(-|\/)\d{2}(-|\/)\d{2}$/g;
	
	if (!patternNome.test(nome.value)){
		$("#" + form.id + " span.errorNome").html("Il nome può contenere solo lettere e numeri e avere una lunghezza tra 2 e 20 caratteri<br/>");
		nome.focus();
		return false;
	}
	
	if (!patternGenere.test(genere.value)){
		$("#" + form.id + " span.errorGenere").html("Il genere può contenere solo lettere e numeri e avere una lunghezza tra 2 e 20 caratteri<br/>");
		genere.focus();
		return false;
	}
	
	if(tipo.value == "serie"){
		episodi = form.episodi;
		
		if (!patternEpisodi.test(episodi.value) || episodi.value <= 0 || episodi.value > 255){
			$("#" + form.id + " span.errorEpisodi").html("Il numero di episodi deve essere un numero compreso tra 1 e 255<br/>");
			episodi.focus();
			return false;
		}
		
	} else if (tipo.value == "film"){
		uscita = form.dataUscita;
		
		if (!patternUscita.test(uscita.value)){
			$("#" + form.id + " span.errorUscita").html("La data d'uscita deve avere tale formato: YYYY/MM/DD<br/>");
			uscita.focus();
			return false;
		}
	} else{ //se il valore di "tipo" non è valido
		return false;
	}
	
	return true;
}

function checkDelForm(){
	var name = del.anim;
	
	var patternName = /^.{2,20}$/g;
	
	if (!patternName.test(name.value)){
		document.getElementById("errorDel").innerHTML = "Nome non valido<br/>";
		name.focus();
		return false;
	}
	
	return true;
}

function checkAddDelStaff(form){
	var codiceFiscale = form.codiceFiscale;
	
	var patternCF = /^\w{16}$/g;
	
	if(!patternCF.test(codiceFiscale.value)){
		$("#" + form.id + " span.errorCF").html("Codice Fiscale non valido<br/>");
		codiceFiscale.focus();
		return false;
	}
	
	
}

function setStaff(animations){
	var animArray = JSON.parse(decodeURIComponent(animations));
	var select = document.getElementById('idStaff');
	var animation = document.querySelector("#idAnimDelStaff").value;

	var staffArray = animArray[animArray.findIndex((el) => el._id == animation)].staffs;
	console.log(staffArray, animation)
	var html = "" + "<option value='' disabled selected>Seleziona un'opzione</option>";
	
	$("#idStaff").empty();
	var option = document.createElement('OPTION');
	option.text = "Seleziona un'opzione";
	option.disabled = true;
	select.add(option);

	staffArray.forEach(staff => {
		var opt = document.createElement('OPTION');
		opt.text = staff.nome + " " + staff.cognome + " | " + staff.ruolo;
		opt.value = staff._id;
		document.getElementById('idStaff').add(opt);
		//html += "<option value='" + staff._id + "'" +  ">" + staff.nome + " " + staff.cognome + " | " + staff.ruolo + "</option>";
	});

	document.querySelector(".staffSelect").style.display = "block";
	document.getElementById('idStaff').style.display = "block";
	//document.getElementById("idStaff").innerHTML = "ciao";
}