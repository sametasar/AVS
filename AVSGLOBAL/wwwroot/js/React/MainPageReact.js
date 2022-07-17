class Mdl_MainPage {

    constructor(Title) {
        this.Title = Title;        
    }
}

var MainPageObject = new Mdl_MainPage();

///Sayfada bulunan kontrolleri ilklendirmemize yarar.
function Init() {
    MainPageObject.Title = "ANA SAYFAYA HOŞ GELDİN";   
}

Init();

///Login form başlığı, LoginObject.Title değeri nuıll ise "AVS GLOBAL SUPPLY Login" yazacak.
class MainPageTitle extends React.Component {
    render() {
        return (
            <div className="row">
                <div className="input-field col s12">
                    <h5 className="ml-4">{MainPageObject.Title ? MainPageObject.Title : 'ANA SAYFAYA HOŞGELDİN'}</h5>
                </div>
            </div>
        )
    }
}


///Login form başlığı, LoginObject.Title değeri nuıll ise "AVS GLOBAL SUPPLY Login" yazacak.
class Tab extends React.Component {
    render() {
        return (
            <div className="row">
                <div className="input-field col s12">
                    <h5 className="ml-4">{MainPageObject.Title ? MainPageObject.Title : 'ANA SAYFAYA HOŞGELDİN'}</h5>
                </div>
            </div>
        )
    }
}


function RenderReact() {

    class MainPage extends React.Component { 

        
        render() {
            
            return (
                <div>
                    <MainPageTitle />
                </div>
            )            
        }
    }

    const container = document.getElementById('root');
    const root = ReactDOM.createRoot(container);

    root.render(<MainPage />);

    //React Çalıştığından emin Olmak İçin
}

RenderReact();