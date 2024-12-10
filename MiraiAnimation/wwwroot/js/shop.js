
function checkForm(form){
	var quantity = form.quantity;
	var spanError = document.getElementById("error" + form.bd.value);
	var patternQuantity = /^\d+$/g;
	
	if (!patternQuantity.test(quantity.value)){
		spanError.innerHTML = "Il campo 'quantità' può assumere solo valori numerici";
		quantity.focus();
		return false;
	}
	
	return true;
}