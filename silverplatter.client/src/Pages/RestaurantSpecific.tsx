import { useState } from "react";
import Gallery from "../Components/Gallery";
import Menu from "../Components/Menu";
import "./css/RestaurantSpecific.css"

function bookTable() {
    // Something
}

function RestaurantSpecific() {
    const [saved, setSaved] = useState<Boolean>(false);
    const [isRestaurantOwner] = useState(true);
    const [bgColor, setBgColor] = useState("#007bff"); // default background
    const [textColor, setTextColor] = useState("#ffffff"); // default text
    const [flexDirection, setFlexDirection] = useState<"row" | "column">("column"); // flexlayout

    let tempRestaurant = {
        id: 0,
        name: "Temporary Restaurant",
        description: "Discover the best temp in temp, all at the small price of 12.99 Temp, you couldn't dream of a better dream than that!",
        address: "Tempstreet 23, Tempköping"
    }

    return (
        <div className={`RestaurantFrontPage custom-theme`}  
            style={{
                "--theme-bg": bgColor,
                "--theme-text": textColor,
                "--flex-direction": flexDirection // CSS variable for layout
            } as React.CSSProperties}
        >
            <div className="RestaurantFrontSection">
                <div className="RestaurantHeader">
                    <h1>{tempRestaurant.name}</h1>
                    <h3 id="Address">{tempRestaurant.address}</h3>
                </div>

                <section className="Information">
                    <div className="Description">
                        <h3>{tempRestaurant.description}</h3>
                    </div>
                    <div className="Accolades">

                    </div>
                </section>

                <section className="BookTable">
                    <button id="BookButton" onClick={() => bookTable()}>
                        <h2>Book Table</h2>
                    </button>
                    <button id="SaveButton" onClick={() => setSaved(!saved)} style={saved ? {backgroundColor: "darkred"} : {}}>
                        <h2>{!saved ? "Save to My Page" : "Remove from My Page"}</h2>
                    </button>
                </section>
            </div>

            {isRestaurantOwner && (
                <section className="EditRestaurant">
                <div>
                    <label>
                    Background color:{" "}
                    <input
                        type="color"
                        value={bgColor}
                        onChange={(e) => setBgColor(e.target.value)}
                    />
                    </label>
                </div>
                <div>
                    <label>
                    Text color:{" "}
                    <input
                        type="color"
                        value={textColor}
                        onChange={(e) => setTextColor(e.target.value)}
                    />
                    </label>
                </div>
                <div>
                    <label>
                        Layout direction:{" "}
                        <select
                            value={flexDirection}
                            onChange={(e) => setFlexDirection(e.target.value as "row" | "column")}
                        >
                            <option value="row">Row</option>
                            <option value="column">Column</option>
                        </select>
                    </label>
                </div>
                </section>
            )}

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