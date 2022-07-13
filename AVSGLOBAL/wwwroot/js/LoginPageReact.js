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

///Sayfada bulunan kontrolleri ilklendirmemize yarar.
function Init() {
    LoginObject.Title = "";
    LoginObject.UserName = "";
    LoginObject.Password = "";
    LoginObject.Remember = "Remember Me";
    LoginObject.Button = "Giriş";
    LoginObject.Register = "Register Now!";
    LoginObject.Forgot = "Forgot password ?";
}

Init();

///Login form başlığı, LoginObject.Title değeri nuıll ise "AVS GLOBAL SUPPLY Login" yazacak.
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

///Kullanıcı adının girileceği UserName komponenti.
class UserName extends React.Component {

    constructor() {
        super();
        this.state = {
            text: ""
        };
    }

    /**
     * TextChange eventi UserName textbox ı değişimini yakalayan eventdir. "onChange={() => this.TextChange(event)}" ile çağrılır.
     * @memberof TextChange
     * @param event Event text kutusunun değişim anındakidurumunu gönderir. İçinde ilgili textboxın veerilerine ulaşabilirsiniz.
     *
     * @return UserName Textbox componentinin constructor içinde bulunan  state durum bilgilerindeki text değerini günceller.
     */
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
                    <label htmlFor="username" className="center-align">Username</label>
                </div>
            </div>
        )
    }
}

///Password kontrolü
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
                    <label htmlFor="password">Password</label>
                </div>
            </div>
        )
    }
}

///Beni hatırla kontrolü
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


///Login butonu kontrolü için geliştirilmiştir.Buton tıklandığında LoginTest Metodu devreye girer. LoginTest Metodu içinde axios ile /login web servisi tetiklenir.
class LoginButton extends React.Component {


    LoginTest() {

        //axios.get("/Login/LoginTest?UserName=" + LoginObject.UserName + "&Password=" + LoginObject.Password + "")
        //    .then(function (response) {
        //        console.log(response.data);
        //        alert("İşlem Tamam");
        //    })
        //    .catch(function (error) {
        //        console.log(error);
        //    });

        axios.get("/login?Email=" + LoginObject.UserName + "&Password=" + LoginObject.Password + "")
            .then(function (response) {
                console.log(response.data);
                window.location.href = "/MainWindow";
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

class Register extends React.Component {
    render() {
        return (
            <div className="input-field col s6 m6 l6">
                <p className="margin medium-small"><a>{LoginObject.Register}</a></p>
            </div>
        )
    }
}


class Forgot extends React.Component {
    render() {
        return (
            <div className="input-field col s6 m6 l6">
                <p className="margin right-align medium-small"><a>{LoginObject.Forgot}</a></p>
            </div>
        )
    }
}

function RenderApp() {

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

window.onload = RenderApp;