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

// Bir dijital ürün ve kullanıcıları birbirini beklemeden (400ms den daha kısa bir sürede) etkileşimde bulunuyorsa, verimlilik dorukta olur.
// setTimeout(function(){

//     RenderReact();

// }, 400); 