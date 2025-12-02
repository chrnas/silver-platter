import PageButton from "../Components/PageButton";
import PageLogo from "../Components/PageLogo";
import './css/Header.css'

function Header() {
    return (
        <div id="Header">
            <PageLogo />
            <div id="Menu">
                <PageButton name="Home" ref="/" />
                <PageButton name="Browse" ref="/Browse" />
                <PageButton name="My Page" ref="/MyPage" />
            </div>
        </div>
    )
}

export default Header;