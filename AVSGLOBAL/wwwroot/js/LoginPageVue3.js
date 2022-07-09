$('#CheckRemember').change(function () {
    if (!$(this).is(':checked')) {
        alert("Beni hatırla pasif");
    }
    else {
        alert("Beni hatırla aktif");
    }
});

const app = Vue.createApp({

    data() {
        return {
            title: "365 SEA SUPPLY LOGIN",
            Token: "",
            RegisterPage: "user-register.html",
            ForgotPassword: "user-forgot-password.html",
            username: "",
            password: ""
        }
    },
    methods: {
        LoginControl() {           

            axios.get("/login?UserName=" + this.username + "&Password=" + this.password + "")
                .then(function (response) {
                    console.log(response.data);
                    window.location.href = "/MainWindow";
                })
                .catch(function (error) {
                    console.log(error);
                });

            
            //Post için gerekli request örnei, daha fazla örnek için dökümantasyona bakabilirsin!
            //axios.post('/login', {
            //    UserName: this.username,
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


        },
        doThis() {
            alert("login");
        },
    },
    mounted() {
        //alert("start page");
    }
});

app.mount('#app');