import Gallery from "../Components/Gallery";
import Menu from "../Components/Menu";
import { restaurantFavoriteService } from "../service/RestaurantFavoriteService";
import type { Restaurant } from "../Types/Restaurant";
import type { RestaurantFavorite } from "../Types/RestaurantFavorite";
import { useEffect, useState } from "react";
import "./css/RestaurantSpecific.css"

function RestaurantSpecific(props : {restaurant : Restaurant}) {
    const [saved, setSaved] = useState<Boolean>(false);
    const [isRestaurantOwner] = useState(true);
    const [bgColor, setBgColor] = useState("#007bff"); // default background
    const [textColor, setTextColor] = useState("#ffffff"); // default text
    const [flexDirection, setFlexDirection] = useState<"row" | "column">("column"); // flexlayout

    /** Check if restaurant is among already favorite restaurants,
     * if it is toggle the save button to highlight as saved. 
     */
    useEffect(() => {
        restaurantFavoriteService.getByUserId(0).then(favorites => {
            let restaurantIds = favorites.map(favorite => {
                return favorite.restaurantId;
            })
            if(props.restaurant.id in restaurantIds) {
                setSaved(true)
            }
        })
    })

    /**
     * Add restaurant to favorites
     * @param restaurant: this restaurant
     */
    function addToFavorites(restaurant : Restaurant) {
        let restId = restaurant.id;
        let userId = 0;
        restaurantFavoriteService.getAll().then(data => {
            let favorite : RestaurantFavorite = {id: data.length, userId: userId, restaurantId: restId}
            restaurantFavoriteService.create(favorite)
        })
    }

    /**
     * Remove restaurant from favorites
     * @param restaurant: this restaurant
     */
    function removeFromFavorites(restaurant : Restaurant) {
        restaurantFavoriteService.delete(restaurant.id)
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
                    <h1>{props.restaurant.name}</h1>
                    <h3 id="Address">{props.restaurant.address}</h3>
                </div>

                <section className="Information">
                    <div className="Description">
                        <h3>{props.restaurant.description}</h3>
                    </div>
                    <div className="Accolades">

                    </div>
                </section>

                <section className="BookTable">
                    <button id="BookButton" onClick={() => console.log("Booked")}>
                        <h2>Book Table</h2>
                    </button>
                    <button id="SaveButton" onClick={() => {
                        if(saved) {
                            removeFromFavorites(props.restaurant);
                        } else {
                            addToFavorites(props.restaurant)
                        }

                        setSaved(!saved)

                    }} style={saved ? {backgroundColor: "darkred"} : {}}>
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