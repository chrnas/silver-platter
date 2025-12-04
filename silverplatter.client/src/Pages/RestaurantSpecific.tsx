import Gallery from "../Components/Gallery";
import Menu from "../Components/Menu";
import "./css/RestaurantSpecific.css"

function bookTable() {
    // Something
}

function RestaurantSpecific() {
    let tempRestaurant = {
        Id: 0,
        Name: "Temporary Restaurant",
        Description: "Discover the best temp in temp, all at the small price of 12.99 Temp, you couldn't dream of a better dream than that!",
        Address: "Tempstreet 23, Tempköping"
    }

    return (
        <div className="RestaurantFrontpage">
            <div className="RestaurantFrontSection">
                <div className="RestaurantHeader">
                    <h1>{tempRestaurant.Name}</h1>
                    <h3 id="Address">{tempRestaurant.Address}</h3>
                </div>

                <section className="Information">
                    <div className="Description">
                        <h3>{tempRestaurant.Description}</h3>
                    </div>
                    <div className="Accolades">

                    </div>
                </section>

                <section className="BookTable">
                    <button id="BookButton" onClick={() => bookTable()}>
                        <h2>Book Table</h2>
                    </button>
                    <button id="SaveButton" onClick={() => bookTable()}>
                        <h2>Save to My Page</h2>
                    </button>
                </section>
            </div>

            <section className="Utilities">
                <div className="Menu">
                    <Menu/>
                </div>

                <div className="Gallery">
                    <Gallery/>
                </div>
            </section>


        
        </div>
    )
}

export default RestaurantSpecific;