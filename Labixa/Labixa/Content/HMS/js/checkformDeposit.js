
var checkDeposit = false;
var formDeposit = $('#formDeposit');
var submitBtnDeposit = $('#btn-submit-depositForm');
var languageDeposit = $('#languageDeposit').val().trim();
//languageDe
//function isValidGmailDeposit(gmailString) {
//    var regex = /^[a-zA-Z0-9_.-]+@@[a-zA-Z0-9]+[a-zA-Z0-9.-]+[a-zA-Z0-9]+.[a-z]{0,4}$/;
//    if (!regex.test(gmailString)) {
//        return false;
//    }
//    return true;
//}
function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
function isValidPhoneDeposit(phoneString) {
    if (phoneString.length != 10) {
        return false;
    }
    return true;
}
$(function () {
    submitFormDeposit();
});
function submitFormDeposit() {
    submitBtnDeposit.click(function (e) {
        checkDeposit = validateFormDeposit();
        if (checkDeposit) {
           
            if (languageDeposit == 'vi') {
                alert("Gửi thông tin kí gởi thành công");
            } else {
                alert("Successfully");
            }
            formDeposit.submit();
        } else {
            e.preventDefault();
        }
    });
}
function validateFormDeposit() {
    $('.error-DepositPhone').addClass('hidden');
    $('.error-DepositEmail').addClass('hidden');
    $('.error-DepositPrice').addClass('hidden');
    var depositName, depositEmail, depositPhone, depositAddress, depositType, depositPrice;
    depositType = $('#depositType').val();
    depositAddress = $('#depositAddress').val();
    depositPrice = $('#depositPrice').val();
    depositName = $('#depositName').val();
    depositEmail = $('#depositEmail').val();
    depositPhone = $('#depositPhone').val();
    if (depositType == "") {
        $('.error-DepositType-none').removeClass('hidden');
        return false;
    } else {
        $('.error-DepositType-none').addClass('hidden');
    }
    if (depositAddress == "") {
        $('.error-DepositAddress-none').removeClass('hidden');
        return false;
    } else {
        $('.error-DepositAddress-none').addClass('hidden');
    }
    if (depositPrice == "") {
        $('.error-DepositPrice-none').removeClass('hidden');
        return false;
    } else {
        $('.error-DepositPrice-none').addClass('hidden');
    }
    if (isNaN(parseInt(depositPrice))) {
        $('.error-DepositPrice').removeClass('hidden');
        return false;
    }

    if (depositName == "") {
        $('.error-DepositName-none').removeClass('hidden');
        return false;
    } else {
        $('.error-DepositName-none').addClass('hidden');
    }
    if (depositPhone == "") {
        $('.error-DepositPhone-none').removeClass('hidden');
        return false;
    } else {
        $('.error-DepositPhone-none').addClass('hidden');
    }
    if (!isValidPhoneDeposit(depositPhone)) {
        $('.error-DepositPhone').removeClass('hidden');
        return false;
    } else {
        $('.error-DepositPhone').addClass('hidden');
    }
    if (depositEmail == "") {
        $('.error-DepositEmail-none').removeClass('hidden');
        return false;
    } else {
        $('.error-DepositEmail-none').addClass('hidden');
    }
    if (!validateEmail(depositEmail)) {
        $('.error-DepositEmail').removeClass('hidden');
        return false;
    } else {
        $('.error-DepositEmail').addClass('hidden');
    }
    return true;
}
  

