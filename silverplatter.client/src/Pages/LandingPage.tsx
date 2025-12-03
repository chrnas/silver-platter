import PopupRestaurantComp from '../Components/PopupRestaurantComp';
import './css/LandingPage.css'

function LandingPage() {
    let tempRestaurant = {
        Id: 0,
        Name: "Temporary Restaurant",
        Description: "Discover the best temp in temp, all at the small price of 12.99 Temp, you couldn't dream of a better dream than that!",
        Address: "tempytemptemptemp"
    }

    return (
        <div className="LandingPage">
            <div className="Content">
                <PopupRestaurantComp restaurant={tempRestaurant}/>
            </div>

            <div className="Background">
                <video autoPlay muted loop> 
                    <source src="src/assets/LandingBackground.mp4" type="video/mp4"/>
                </video>
            </div>
        </div>
    )
}

export default LandingPage;