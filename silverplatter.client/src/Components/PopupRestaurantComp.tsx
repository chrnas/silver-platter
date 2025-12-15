
import type { Restaurant } from '../Types/Restaurant';
import './css/popupRestaurantComp.css'



function PopupRestaurantComp(props : { restaurant : Restaurant}) {

    return (
        <div className='Frame'>
            <div className="PopupRestaurant">
                <div className='Info'>
                    <div>
                        <h1 className='PR_Name'>{props.restaurant.name}</h1>
                        <p className='PR_Description'>{props.restaurant.description}</p>
                    </div>
                </div>
                <img
                    src="src/assets/pexels-life-of-pix-67468.jpg" 
                />
            </div>
        </div>
    )
}

export default PopupRestaurantComp;