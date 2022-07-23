
 /**
  Bu class Login sayfasında bulunan kontrollerdeki yazıları tek noktadan yönetmemizi sağlar.
  @memberof Cls_Login
  @property {string}   Title       login sayfasının başlık bilgisini tutar.
  @property {string}   UserName    Login sayfasında kullanıcı adı kontrolüne girilen değerleri tutar.
  @property {string}   Password    Login sayfasında Password kontrolüne girilen değeri tutar.
  @property {string}   Remember    Beni hatırla bilgisini barındırır.
  @property {string}   Button      Login buton bilgilerini üzerinde tutar.
  @property {string}   Register    Register bilgisini tutar.
  @property {string}   Forgot      Şifremi unuttum bilgisini üzerinde tutar.
  */
class Cls_Login {

    constructor(Title, UserName, Password, Remember,Button,Register,Forgot) {
        this.Title = Title;
        this.UserName = UserName;
        this.Password = Password;
        this.Remember = Remember;
        this.Button = Button;
        this.Register = Register;
        this.Forgot = Forgot;
    }
}

var LoginObject = new Cls_Login();

///Sayfa ilk açıldığında çalışır sayfa değişkenlerini ilklendirir.
function Init() {
    LoginObject.Title = "AVS GLOBAL SUPPLY " +TranslateControl(2) +" "+ TranslateControl(9);
    LoginObject.UserName = "";
    LoginObject.Password = "";
    LoginObject.Remember = TranslateControl(5);
    LoginObject.Button = TranslateControl(2);
    LoginObject.Register = TranslateControl(6);
    LoginObject.Forgot = TranslateControl(4);
}

Init();

///Başlık komponenti.Login sayfasının başlık bilgisi buarada belirlenir.
class LoginTitle extends React.Component {
    render() {
        return (
            <div className="row">
                <div className="input-field col s12">
                    <h5 className="ml-4">{LoginObject.Title ? LoginObject.Title : 'AVS GLOBAL SUPPLY Login'}</h5>
                </div>
            </div>
        )
    }
}

///Kullanıcı adı girişinin yapıldığı textbox kontrolü.
class UserName extends React.Component {

    constructor() {
        super();
        this.state = {
            text: ""
        };
    }

    TextChange(event) {
        this.setState((prevState, props) => {

            if (event != undefined) {
                LoginObject.UserName = event.target.value;
                return { text: event.target.value };
            }
        });
    }

    render() {
        return (
            <div className="row margin">
                <div className="input-field col s12">
                    <i className="material-icons prefix pt-2">person_outline</i>
                    <input id="username" type="text" onChange={() => this.TextChange(event)} />
                    <label htmlFor="username" className="center-align">{TranslateControl(3)}</label>
                </div>
            </div>
        )
    }
}

///Kullanıcı şifresinin yazıldığı komponent.
class Password extends React.Component {

    constructor() {
        super();
        this.state = {
            text: ""
        };
    }

    TextChange(event) {
        this.setState((prevState, props) => {

            if (event != undefined) {
                LoginObject.Password = event.target.value;
                return { text: event.target.value };
            }

        });
    }

    render() {
        return (
            <div className="row margin">
                <div className="input-field col s12">
                    <i className="material-icons prefix pt-2">lock_outline</i>
                    <input id="password" type="password" onChange={() => this.TextChange(event)}/>
                    <label htmlFor="password">{TranslateControl(8)}</label>
                </div>
            </div>
        )
    }
}

///Beni hatırla kontrolüne ait komponent.
class Remember extends React.Component {
    render() {
        return (
            <div className="row">
                <div className="col s12 m12 l12 ml-2 mt-1">
                    <p>
                        <label>
                            <input id="CheckRemember" type="checkbox" />
                            <span>{LoginObject.Remember}</span>
                        </label>
                    </p>
                </div>
            </div>
        )
    }
}

///Giriş butonuna ait komponent.
class LoginButton extends React.Component {

    LoginTest() {

        axios.get("/login?Email=" + LoginObject.UserName + "&Password=" + LoginObject.Password + "")
            .then(function (response) {
                console.log(response.data);
                window.location.href = AppAddress+"/mainwindow";
            })
            .catch(function (error) {
                console.log(error);
            });
    }

    render() {
        return (
            <div className="row">
                <div className="input-field col s12">
                    <button id="btnLogin" className="btn waves-effect waves-light border-round gradient-45deg-purple-deep-orange col s12" onClick={this.LoginTest}>{LoginObject.Button}</button>
                </div>
            </div>
        )
    }
}

///Kayıt butonuna ait komponenet.
class Register extends React.Component {
    render() {
        return (
            <div className="input-field col s6 m6 l6">
                <p className="margin medium-small"><a>{LoginObject.Register}</a></p>
            </div>
        )
    }
}

///Şifremi unuttum kontrolüne ait komponent.
class Forgot extends React.Component {
    render() {
        return (
            <div className="input-field col s6 m6 l6">
                <p className="margin right-align medium-small"><a>{LoginObject.Forgot}</a></p>
            </div>
        )
    }
}

///React komponentlerinin render edilmesini ve sayfa eklenmesini gerçekleştiren metot.
function RenderReact() {

    class LoginForm extends React.Component {

        render() {
            return (
                <div className="login-form">

                    <LoginTitle />

                    <UserName />

                    <Password />

                    <Remember />

                    <LoginButton />

                    <div className="row">
                        <Register />
                        <Forgot />
                    </div>
                </div>
            )
        }
    }

    const container = document.getElementById('root');
    const root = ReactDOM.createRoot(container);
    root.render(<LoginForm />);
}

RenderReact();