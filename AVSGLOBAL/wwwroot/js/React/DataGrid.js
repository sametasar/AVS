function GetData()
{
    if($("#table").length>0)
    {
        axios.get("/User/Get_Users")
        .then(function (response) {        
            DATATABLERUN(response);
            
        })
        .catch(function (error) {
            console.log(error);
        });
    }
    else
    {
        setTimeout(function() {           
            GetData();
        }, 100);
    }    
}
class DataGrid extends React.Component{

    render(){

        return(            
            <div id="table" onLoad = {GetData()}>

            </div>
        ) 
    }
}


function RenderReact() {

    class MainPage extends React.Component { 

        render() {
            
            return (
                <div>
                    <DataGrid />      
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