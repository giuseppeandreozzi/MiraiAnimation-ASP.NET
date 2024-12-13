
function checkForm(){
	var name = formUser.nome;
	var surname = formUser.cognome;
	var pass = formUser.password;
	var address = formUser.indirizzo;
	var birthDate = formUser.dataNascita;
	var spanError;
	
	var patternName = /^[a-zA-Z]{2,20}$/g;
	var patternSurname = /^[a-zA-Z]{2,20}$/g;
	var patternPass = /^[a-zA-Z0-9._-]{4,15}$/g;
	var patternAddress = /^[a-zA-Z0-9 ]{1,30}$/g;
	var patternBirthDate = /^\d{4}(-|\/)\d{2}(-|\/)\d{2}$/g;
	
	if (!patternName.test(name.value)){
		spanError = document.getElementById("errorName");
		spanError.innerHTML = "Il nome deve essere lungo almeno 2 caratteri e massimo 20, inoltre può contenere solo lettere";
		name.focus();
		return false;
	}
	
	if (!patternSurname.test(surname.value)){
		spanError = document.getElementById("errorSurname");
		spanError.innerHTML = "Il cognome deve essere lungo almeno 2 caratteri e massimo 20, inoltre può contenere solo lettere";
		surname.focus();
		return false;
	}
	
	if (pass.value != "key" && !patternPass.test(pass.value)){
		spanError = document.getElementById("errorPass");
		spanError.innerHTML = "La password deve essere lunga almeno 4 caratteri e massimo 15,<br/>" +
			"inoltre può contenere solo lettere";
		pass.focus();
		return false;
	}
	
	if(!patternAddress.test(address.value)){
		spanError = document.getElementById("errorAddress");
		spanError.innerHTML = "L'indirizzo può contenere solo lettere e deve essere lungo massimo 30 caratteri";
		address.focus();
		return false;
	}
	
	if(!patternBirthDate.test(birthDate.value)){
		spanError = document.getElementById("errorBirthDate");
		spanError.innerHTML = "La data di nascita deve avere un formato del tipo YYYY/MM/DD";
		birthDate.focus();
		return false;
	}
	
	return true;
}