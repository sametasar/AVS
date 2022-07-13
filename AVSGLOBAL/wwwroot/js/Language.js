var SelectLangugeStr = "";
var LanguageList = "";
var LanguageID = "";

function DefaultLanguage() //Site İlk açıldığında
{
    //Language cookiesi eğer sitede yoksa bu metot devreye girecek ve default olan dili seçmiş olacak.

    //Hemen ardından seçilen dil aşağıdaki SelectLanguage adlı metot ile siteye uygulanacak.

    //Her sayfanın kendi js dosyasında bulunan Language() adlı metot devreye girecek ve o sayfanın kontrollerini geçerli dil olarak değiştirecek.
    
    $.ajax({
        type: "get",
        url: "" + ServisAdresi + "Language/DefaultLanguage",
        dataType: "text",
        success: function (data) {
            Language(data);
        }
    });
}

function Language(LanguageParameter) {
    LanguageID = LanguageParameter;
        $.ajax({
            type: "get",
            url: "/language/Translate",
            dataType: "JSON",
            success: function (data) {
                     
                LanguageList = JSON.parse(data.toString());
                      
                setCookie('Language', LanguageParameter, -1); //Delete Cookie
                setCookie('Language', LanguageParameter, 1); //Fill Cookie

                try { $("#txtLanguage").val(LanguageList[0].Name); } catch (e) { }  //Login Page #txtLanguage Control 

                var myElements = $(".Language");
                for (var x = 0; x < myElements.length; x++) {

                    var ControlName = "";

                    ControlName = myElements.eq(x).attr("id");

                    $.each(LanguageList, function (i, item) {

                        if (item.ControlID.toString() == myElements.eq(x).attr("ControlID")) {
                            if (myElements.eq(x).attr("placeholder") == null) {
                                try {
                                    $("#" + ControlName + "").html(item.Word.toString());
                                } catch (err) { }

                                try {
                                    $("#" + ControlName + "").val(item.Word.toString());
                                } catch (err) { }

                                try {
                                    $("#" + ControlName + "").text(item.Word.toString());
                                } catch (err) { }
                            }
                            else {
                                myElements.eq(x).attr("placeholder", item.Word.toString());
                            }
                        }
                    });

                };

            }
        });                 
    }

function Translate(ControlID) {
        var Response = 'Null';

        /*#region LanguageList Boş İse  */
           
        if (LanguageList == null || LanguageList == "" || LanguageList == "undefined") {

            if (LanguageID == null || LanguageID == "" || LanguageID == "undefined") {

                DefaultLanguage();
            }
            else {
                Language(LanguageID);
            }

        }      
         
        /*#endregion*/

          
        $.each(LanguageList, function (i, item) {

            if (item.ControlID.toString() == ControlID) {
                Response = item.Word.toString();
            }
        });

        return Response;
    }

//################################################Language################################################