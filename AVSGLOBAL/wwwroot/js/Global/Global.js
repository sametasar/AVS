var TabloSayi =0;

function Title(text)
{
   if($(".PageTitle").length>0)
   {
        $(".PageTitle").html(text);      
   }
   else
   {
    setTimeout(function() {           
        Title();
    }, 100);
   }    
}

function TableToJson(Table) {
debugger;
    var $table = Table;
    rows = [],
        header = [];

    $table.find("thead th").each(function () {
        header.push($(this).html());
    });

    $table.find("tbody tr").each(function () {
        var row = {};

        $(this).find("td").each(function (i) {
            var key = header[i],
                value = $(this).html();

            row[key] = value;
        });

        rows.push(row);
    });

    return JSON.stringify(rows);
}

function DATATABLERUN(result)
{

alert("deneme");
debugger;

  let ResultReport =
  '<table id="DataGrid' +
  TabloSayi.toString() +
  '" class="table"> ' +
  "<thead>                                               " +
  "    <tr>                                              " +
  "    @Columns1                                         " +
  "    </tr>                                             " +
  "</thead>                                              " +
  "<tbody>                                               " +
  "    @Body                                             " +
  "</tbody>                                              " +
  " <tfoot>                                              " +
  "    <tr>                                              " +
  "   @Columns2                                           " +
  "    </tr>                                             " +
  "</tfoot>                                              " +
  "</table>";

//result içinden Body ve Columns bilgilerini oluştur!

let Body = "";
let Columns = "";
let Data = result.data;


/* #region  KOLON BİLGİLERİNİ ALIRIM */

if(Data.data!=undefined)
{

  for (let key in Data.data[0]) {
    Columns += "<th>" + key + "</th>";
  }
}
else
{

  for (let key in Data[0]) {
   
    Columns += "<th>" + key + "</th>";
  }
}


ResultReport = ResultReport.replace("@Columns1", Columns);
ResultReport = ResultReport.replace("@Columns2", Columns);

/* #endregion */

/* #region  DATALAR YÜKLENİR */

if(Data!=undefined)
{
  for (let i = 0; i < Data.length; i++) {
    Body += "<tr>";
    for (let key in Data[i]) {
      Body += "<td>" + Data[i][key] + "</td>";
    }
    Body += "</tr>";
  }
}

ResultReport = ResultReport.replace("@Body", Body);
/* #endregion */

$("#table").html(ResultReport);

Table = $("#DataGrid" + TabloSayi.toString()).DataTable({

  select: true,
  // "select": {
  //       style: 'multi'
  //   },
  autoFill: true,

  // colReorder: true,
  // scrollY:        "300px",
  // scrollX:        true,
  // scrollCollapse: true,
  // deferRender:    true,
  // scrollCollapse: true,
  // scroller:       true,
  paging: true,
  // fixedColumns:   {
  //     leftColumns: 2
  // },
  //     columnDefs: [ {
  //   targets: 2,
  //   render: $.fn.dataTable.render.moment( 'YYYY/MM/DD', 'Do MMM YY', 'tr' )
  // } ],
  // fixedHeader: true,
  // searching: true,
  keys: true,
  ordering: true,
  order: [[1, "asc"]],
  processing: true,
  // rowReorder: true,
  // drawCallback: function (settings) {
  //         $("#RaporContentx2").html();

  //     },
  pagingType: "full_numbers",
  orderCellsTop: true,
  language: {
    lengthMenu: "",
    zeroRecords: "Kayıt bulunamadı!",
    info: "Görüntülenen Sayfa _PAGE_ - Toplam _PAGES_ Sayfa",
    infoEmpty: "Kayıt bulunamadı",
    search: "Ara",
    emptyTable: "Kayıt bulunamadı!",
    infoPostFix: "",
    thousands: ",",
    loadingRecords: "Yükleniyor...",
    processing: "İşleniyor...",
    infoFiltered: "(_MAX_ Kayıt Filtrelendi)",
    paginate: {
      first: "İlk",
      last: "Son",
      next: "Sonraki",
      previous: "Önceki",
    },
    aria: {
      sortAscending: ": A dan Z ye kayıtlar listelendi",
      sortDescendin: ": Z den A ya kayıtlar sıralandı",
    },
  },
  dom: "Bfrtip",
  buttons: [
    "copy",
    "csv",
    "excel",
    "pdf",
    "print",
    {
      text: "EXCEL AKTAR",
      key: {
        shiftKey: true,
        key: "1",
      },
      action: function (e, dt, node, config) {
        alert("Button 2 activated");
      },
    },
    {
      text: "JSON",
      key: {
        shiftKey: true,
        key: "2",
      },
      action: function (e, dt, node, config) {
        alert("Button 2 activated");
      },
    },
    {
      text: "EKLE (Shift+3)",
      key: {
        shiftKey: true,
        key: "3",
      },
      action: function (e, dt, node, config) {
        alert("Button 3 activated");
      },
    },

    {
      text: "DÜZENLE (Shift+4)",
      key: {
        shiftKey: true,
        key: "4",
      },
      action: function (e, dt, node, config) {
        alert("Button 4 activated");
      },
    },
    {
      text: "SİL (Shift+5)",
      key: {
        shiftKey: true,
        key: "5",
      },
      action: function (e, dt, node, config) {
        alert("Button 5 activated");
      },
    },
    {
      text: "YENİLE (Shift+6)",
      key: {
        shiftKey: true,
        key: "6",
      },
      action: function (e, dt, node, config) {
        alert("Button 6 activated");
      },
    },
  ],

  initComplete: function (settings, json) {},
});

//Add event listener for opening and closing details
$("#DataGrid" + TabloSayi + " tbody").on(
  "click",
  "td.details-control",
  function () {
    let tr = $(this).closest("tr");
    let row = Table.row(tr);

    if (row.child.isShown()) {
      // This row is already open - close it
      $("div.slider", row.child()).slideUp(function () {
        row.child.hide();
        tr.removeClass("shown");
      });
    } else {
      // Open this row
      row.child(format(row.data()), "no-padding").show();
      tr.addClass("shown");

      $("div.slider", row.child()).slideDown();
    }
  }
);

$("#DataGrid" + TabloSayi + " tbody").on("click", "tr", function () {
  if ($(this).hasClass("selected")) {
    $(this).removeClass("selected");
  } else {
    Table.$("tr.selected").removeClass("selected");
    $(this).addClass("selected");
  }
});

//$('#button').click(function () {
//    table.row('.selected').remove().draw(false);
//});

//Materialize.toast('<span>Raporunuz Oluşturuluyor lütfen biraz bekleyin!</span>', 3000);

$("#DataGrid" + TabloSayi + " tfoot th").each(function () {
  let title = $(this).text();
  $(this).html('<input type="text" placeholder="ARA ' + title + '" />');
});

Table.columns().every(function () {
  let that = this;

  $("input", this.footer()).on("keyup change clear", function () {
    if (that.search() !== this.value) {
      that.search(this.value).draw();
    }
  });
});

// Setup - add a text input to each footer cell
//$('#DataGrid' + TabloSayi+ ' thead tr').clone(true).appendTo('#DataGrid thead');
//$('#DataGrid' + TabloSayi + ' thead tr:eq(1) th').each(function (i) {
//    var title = $(this).text();
//    $(this).html('<input type="text" placeholder="Search ' + title + '" />');

//    $('input', this).on('keyup change', function () {
//        if (Table.column(i).search() !== this.value) {
//            Table
//                .column(i)
//                .search(this.value)
//                .draw();
//        }
//    });
//});

Table.on("key-focus", function (e, datatable, cell, originalEvent) {
  let rowData = datatable.row(cell.index().row).data();

  $("#details").html("Seçilen kayıt : " + rowData[0]);
}).on("key-blur", function (e, datatable, cell) {
  $("#details").html("Hücre seçilmedi!");
});

}

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