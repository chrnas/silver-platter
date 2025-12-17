import Gallery from "../Components/Gallery";
import Menu from "../Components/Menu";
import { restaurantFavoriteService } from "../service/RestaurantFavoriteService";
import type { Restaurant } from "../Types/Restaurant";
import type { RestaurantFavorite } from "../Types/RestaurantFavorite";
import { useEffect, useRef, useState } from "react";
import "./css/RestaurantSpecific.css"
import type { BookableTable } from "../Types/BookableTable";
import { bookingTableService } from "../service/BookingTableService";

function RestaurantSpecific(props : {restaurant : Restaurant}) {
    const [bookableTables, setBookableTables] = useState<BookableTable[]>([]);
    const [saved, setSaved] = useState<Boolean>(false);
    const [isRestaurantOwner] = useState(false);
    const [bgColor, setBgColor] = useState("#b28f65ff"); // default background
    const [textColor, setTextColor] = useState("#ffffff"); // default text
    const [flexDirection, setFlexDirection] = useState<"row" | "column">("column"); // flexlayout

    const bookingListRef = useRef<HTMLDivElement>(null);

    const scrollToBookingList = () => {
        if (bookingListRef.current) {
        bookingListRef.current.scrollIntoView({ behavior: "smooth" });
        }
    };

    /** Check if restaurant is among already favorite restaurants,
     * if it is toggle the save button to highlight as saved. 
     */
    useEffect(() => {
        if (!props.restaurant) return;

        bookingTableService.getByRestaurantId(props.restaurant.id).then(tables => {
            setBookableTables(tables);
        });

        restaurantFavoriteService.getByUserId(1)
            .then(favorites => {
                const restaurantIds = favorites.map(f => f.restaurantId);

                console.log("List of ids:", restaurantIds);
                console.log("Props id:", props.restaurant.id);

                setSaved(restaurantIds.includes(props.restaurant.id));
            })
            .catch(err => console.error("Failed to load favorites", err));

    }, [props.restaurant.id]);


    /**
     * Add restaurant to favorites
     * @param restaurant: this restaurant
     */
    function addToFavorites(restaurant: Restaurant) {
        const favorite: Omit<RestaurantFavorite, "id"> = {
            userId: 1, 
            restaurantId: restaurant.id
        };

        restaurantFavoriteService.create(favorite)
            .then(() => setSaved(true))
            .catch(err => console.error(err));
    }

    /**
     * Remove restaurant from favorites
     * @param restaurant: this restaurant
     */
    function removeFromFavorites(restaurant : Restaurant) {
        restaurantFavoriteService.deleteByUserAndRestaurant(1, restaurant.id).then(() => setSaved(false));
    }

    function bookTable(table: BookableTable) {
        table.booked = true;
        bookingTableService.update(table).then(table => {
            const filteredTables = bookableTables.filter(t => t.id !== table.id);
            setBookableTables([...filteredTables, table].sort((a,b) => a.id - b.id))
        });
    }

    function unBookTable(table: BookableTable) {
        table.booked = false;
        bookingTableService.update(table).then(table => {
            const filteredTables = bookableTables.filter(t => t.id !== table.id);
            setBookableTables([...filteredTables, table].sort((a,b) => a.id - b.id))
        });
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
                    <button id="BookButton" onClick={scrollToBookingList}>
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
                    <Menu restaurantId={props.restaurant.id}/>
                </div>

                <div className="Gallery">
                    <Gallery/>
                </div>
            </section>

            <section ref={bookingListRef}>
                {bookableTables.map((table) => (
                    <div key={table.id}>
                    <h3>Book table tonight, {table.name} in {table.description} for {table.places} people</h3>
                    {!table.booked && (
                        <button onClick={() => bookTable(table)}>
                        Book now
                        </button>
                    )}
                    {table.booked && (
                        <button onClick={() => unBookTable(table)}>
                        Unbook
                        </button>
                    )}
                    </div>
                ))}
            </section>


        
        </div>
    )
}

export default RestaurantSpecific;