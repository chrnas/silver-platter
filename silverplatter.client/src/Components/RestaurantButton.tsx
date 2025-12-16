import { useNavigate } from 'react-router-dom';
import type { Restaurant } from '../Types/Restaurant';
import './css/RestaurantButton.css'

function RestaurantButton(props : { restaurant : Restaurant}) {
    let nav = useNavigate();
    return (
        <button onClick={() => nav("/Restaurants/" + props.restaurant.name)} id='SingleRestaurantButton'>
            <h2 className='RestaurantName'>{props.restaurant.name}</h2>
            <div className={"restaurant-row"}>
                <div className={"badge"}>
                    rating: {props.restaurant.rating}
                </div>
                <div className={"badge"}>
                    budget: {props.restaurant.budget}
                </div>
            </div>
        </button>
    )
}

export default RestaurantButton;