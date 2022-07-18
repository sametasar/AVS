function TabContent0AfterRender()
{
    if($("#Vessel").length>0)
    {
        $('#Vessel').formSelect();
        $('#OrderType').formSelect();
        $('#Country').formSelect();
        $('#Port').formSelect();
    }
    else
    {
        setTimeout(function() {           
            TabContent0AfterRender();
        }, 100);
    }    
}

class TabContent0 extends React.Component {

    constructor(Captain,CaptainID,Chef,ChefID) {        
        super();
        this.Captain = Captain;
        this.CaptainID = CaptainID;
        this.Chef = Chef;
        this.ChefID = ChefID;            
        this.state = {          
          Title: "CREATE REQUEST"
        };
      }
      
    
     
    render() {  
        
        this.Captain = "SAMET ASAR";
        this.Chef = "MESUT HAS";

        return (
            <div className="row" onLoad = {TabContent0AfterRender()}>
                <div className="clear"></div>
                    <div className="row">
                        <div className="input-field col l4 m6 s12 section scrollspy">
                            <select id="Vessel">
                            <option value="" disabled selected>Vessel Select</option>
                            <option value="1">Nordic Gemi1</option>
                            <option value="2">Nordic Gemi2</option>
                            <option value="3">Nordic Gemi3</option>
                            </select>
                            <label>Vessel Select</label>
                        </div>
                        <div className="input-field col l4 m6 s12">
                            <input id="ImoNumber" readonly="true" type="text" className="validate" />
                            <label for="ImoNumber" className="active">Imo Number</label>
                        </div>

                        <div className="input-field col l4 m6 s12 section scrollspy">
                            <select id="OrderType">
                            <option value="" disabled selected>Order Type Select</option>
                            <option value="1">Main Provision</option>
                            <option value="2">Fresh Provision</option>
                            <option value="3">Bounded</option>
                            <option value="4">Cabine</option>
                            <option value="5">Cabine Sale</option>
                            </select>
                            <label>Order Type Select</label>
                        </div>
                        
                        <div className="input-field col l4 m6 s12">
                            <input id="Captain" readonly="true" type="text" value={this.Captain} className="validate" />
                            <label for="Captain" className="active">Captain</label>
                        </div>

                        <div className="input-field col l4 m6 s12">
                            <input id="Chef" readonly="true" type="text" value={this.Chef} className="validate" />
                            <label for="Chef" className="active">Chef</label>
                        </div>

                        <div className="input-field col l4 m6 s12 section scrollspy">
                            <select id="Country">
                            <option value="" disabled selected>Select Country</option>
                            <option value="1">Turkey</option>
                            <option value="2">Itally</option>
                            <option value="3">Usa</option>
                            <option value="4">China</option>
                            <option value="5">France</option>
                            </select>
                            <label>Select Country</label>
                        </div>

                        <div className="input-field col l4 m6 s12 section scrollspy">
                            <select id="Port">
                            <option value="" disabled selected>Select Port</option>
                            <option value="1">Port 1</option>
                            <option value="2">Port 2</option>
                            <option value="3">Port 3</option>
                            <option value="4">Port 4</option>
                            <option value="5">Port 5</option>
                            </select>
                            <label>Select Port</label>
                        </div>

                        <div className="input-field col l4 m6 s12">
                            <input id="ETA" type="text" class="datepicker" />
                        </div>

                        <div className="input-field col l4 m6 s12">
                            <input id="ETD" type="text" class="datepicker" />
                        </div>
                        
                        <div className="input-field col l4 m6 s12">
                            <input id="NOC" type="text" value="" className="validate" />
                            <label for="NOC" className="active">NUMBER OF CREW</label>
                        </div>

                    </div>
            </div>
        )
    }
}

class TabContent1 extends React.Component {

    constructor() {        
        super();
        this.state = {          
          Title: "SAVED REQUEST"
        };
      }      

    render() {      
        return (
            <div className="row">
                <div className="input-field col s12">
                    <h6 className="ml-4">{this.state.Title ? this.state.Title : 'BAŞLIK YAZISI YOK!'}</h6>
                </div>
            </div>
        )
    }
}

class TabContent2 extends React.Component {

    constructor() {        
        super();
        this.state = {          
          Title: "CREW / GUEST LİST"
        };
      }      

    render() {      
        return (
            <div className="row">
                <div className="input-field col s12">
                    <h6 className="ml-4">{this.state.Title ? this.state.Title : 'BAŞLIK YAZISI YOK!'}</h6>
                </div>
            </div>
        )
    }
}

class TabContent3 extends React.Component {

    constructor() {        
        super();
        this.state = {          
          Title: "ORDERS"
        };
      }      

    render() {      
        return (
            <div className="row">
                <div className="input-field col s12">
                    <h6 className="ml-4">{this.state.Title ? this.state.Title : 'BAŞLIK YAZISI YOK!'}</h6>
                </div>
            </div>
        )
    }
}

class Cls_Tab {
    constructor(ID, Text) {
      this.ID = ID;
      this.Text = Text;
    }
  }

  var TabControl = [];

  var TabObject1 = new Cls_Tab();
  TabObject1.ID = "Page1";
  TabObject1.Text = "Create Request";

  var TabObject2 = new Cls_Tab();
  TabObject2.ID = "Page2";
  TabObject2.Text = "Saved Request";

  var TabObject3 = new Cls_Tab();
  TabObject3.ID = "Page3";
  TabObject3.Text = "Crew / Guest List";

  var TabObject4 = new Cls_Tab();
  TabObject4.ID = "Page4";
  TabObject4.Text = "Orders";

TabControl.push(TabObject1);
TabControl.push(TabObject2);
TabControl.push(TabObject3);
TabControl.push(TabObject4);

function ActiveTab(index)
{
    if(index==1)
    {
        return 'className="active"';
    }
    else
    {
        return '';
    }
}

function TabMethod()
{    
    if($("#TabControl").length>0)
    {
        $('#TabControl').tabs();      
    }
    else
    {
        setTimeout(function() {           
            TabMethod();
        }, 100);
    }        
}

///Login form başlığı, LoginObject.Title değeri nuıll ise "AVS GLOBAL SUPPLY Login" yazacak.
class Tab extends React.Component {

    GetTabContent(index)
    {
        switch(index)
        {
            case 0:
                return <TabContent0></TabContent0>;
            break;

            case 1:
                return <TabContent1></TabContent1>;

            case 2:
                return <TabContent2></TabContent2>;
            break;

            case 3:
                return <TabContent3></TabContent3>;
            break;

        }
    }

    render() {
        return (
            <div id="TabContainer" className="row">
                <div className="input-field col s12">
                <div className="row">
                    <div className="col s12">
                        <ul id="TabControl" className="tabs" onLoad = {TabMethod()}>
                            {TabControl.map((item, index) => (  
                                <li key={index} id={index.toString()} className="tab col s3"><a href={'#'+item.ID}>{item.Text}</a></li>
                            ))}
                    </ul>
                    </div>
                    
                    {TabControl.map((item, index) => (  
                                <div id={item.ID} className="col s12">
                                    {this.GetTabContent(index)}
                                </div>
                            ))}
                </div>
                </div>
            </div>
        )
    }
}

function RenderReact() {

    class MainPage extends React.Component { 
        
        constructor() {        
            super();
            this.state = {          
                Title: "CREATE REQUEST"
            };
        }
        
        render() { 
            Title('AVS CATERING MAIN MENU');
            return (
                <div>                   
                    <Tab></Tab>
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