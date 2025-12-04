import type { Restaurant } from '../Types/Restaurant';
import './css/RestaurantButton.css'

function RestaurantButton(props : { restaurant : Restaurant}) {
    return (
        <div className='Button'>
            {props.restaurant.Name}
        </div>
    )
}

export default RestaurantButton;