import type { Restaurant } from '../Types/Restaurant';
import './css/RestaurantButton.css'

function RestaurantButton(props : { restaurant : Restaurant}) {
    return (
        <div id='SingleRestaurantButton'>
            <h2 className='RestaurantName'>{props.restaurant.Name}</h2>
        </div>
    )
}

export default RestaurantButton;