function loadScriptJs(url, callback) {

      //Kütüphane daha önce eklendiyse bir daha ekleme!
     
      
        // adding the script tag to the head as suggested before
        let head = document.getElementsByTagName("head")[0];
        let script = document.createElement("script");
        script.type = "text/javascript";
        script.src = url;
  
        script.onreadystatechange = callback;
        script.onload = callback;
  
        // fire the loading
        head.appendChild(script);
      
  }

  function loadScriptBabel(url, callback) {

      //Kütüphane daha önce eklendiyse bir daha ekleme!      
      
        // adding the script tag to the head as suggested before
        let head = document.getElementsByTagName("head")[0];
        let script = document.createElement("script");
        script.type = "text/babel";
        script.src = url;
  
        script.onreadystatechange = callback;
        script.onload = callback;
  
        // fire the loading
        head.appendChild(script);
      
  }


class Mdl_Control {
    constructor(ControlID,ControlName) {
        this.ControlID = ControlID;
        this.ControlName = ControlName;       
    }
}

class Mdl_LanguageDictionary {
    constructor(ID,ControlID,LanguageID,Word) {
        this.ID = ID;
        this.ControlID = ControlID;
        this.LanguageID = LanguageID;
        this.Word = Word;    
    }
}

var SelectLanguageID =1; //1 DefaultID Türkçe 2 İngilizce Language veritabanına bakabilirsin!

var AppAddress = "https://localhost:7268";

var LanguageDictionary =[];

function Languageinit()
{
    //todo axios ile request at ve gelen cevapla LanguageDictionary listeni yani sözlüğünü doldur! Sonra alt classlarda kullan.
    
//axios.get("/Login/LoginTest?UserName=" + LoginObject.UserName + "&Password=" + LoginObject.Password + "")
        //    .then(function (response) {
        //        console.log(response.data);
        //        alert("İşlem Tamam");
        //    })
        //    .catch(function (error) {
        //        console.log(error);
        //    });

        axios.get("/GetLanguageDictionary")
            .then(function (response) {
                                
                for(var i=0; i<response.data.length; i++)
                {
                    var Word = new Mdl_LanguageDictionary();
                    Word.ID = response.data[i].id;
                    Word.ControlID = response.data[i].controlID;
                    Word.LanguageID = response.data[i].languageID;
                    Word.Word = response.data[i].word;   
                    LanguageDictionary.push(Word);
                }

            })
            .catch(function (error) {
                console.log(error);
            });

            //Post için gerekli request örnei, daha fazla örnek için dökümantasyona bakabilirsin!
            //axios.post('/login', {
            //    Email: this.username,
            //    Password: this.password
            // })
            // .then(function (response) {
            //     console.log(response.data);
            //        window.location.href = "/Home/MainWindow";
            // })
            // .catch(function (error) {
            //     console.log(error);
            //     alert("Şifre yanlış!");
            // });
}

//Tüm Kelimleeri doldurmasını Söylüyorum.
Languageinit();

//Kullanıcının LocalStorage ında seçili dili varmı?
if(localStorage.getItem("SelectLanguageID")!=undefined && localStorage.getItem("SelectLanguageID")!=null)
{
    //Varsa SelectLanguageID default değerini kullanıcının seçimi ile ezerim!
    SelectLanguageID = parseInt(localStorage.getItem("SelectLanguageID"));
}

function TranslateControl(ControlID)
{
    for(var i =0; i<LanguageDictionary.length; i++)
    {
        if(LanguageDictionary[i].ControlID==ControlID && LanguageDictionary[i].LanguageID==SelectLanguageID)
        {
           return LanguageDictionary[i].Word;
        }
    }
}


function Onstart()
{
      //Bir dijital ürün ve kullanıcıları birbirini beklemeden (400ms den daha kısa bir sürede) etkileşimde bulunuyorsa, verimlilik dorukta olur.
      setTimeout(function(){

        RenderReact();

    }, 400); 
}