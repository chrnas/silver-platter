import { useNavigate } from 'react-router-dom';
import type { Restaurant } from '../Types/Restaurant';
import './css/RestaurantButton.css'

function RestaurantButton(props : { restaurant : Restaurant}) {
    let nav = useNavigate();
    let ref = "/Template"
    return (
        <button onClick={() => nav(ref)} id='SingleRestaurantButton'>
            <h2 className='RestaurantName'>{props.restaurant.name}</h2>
        </button>
    )
}

export default RestaurantButton;