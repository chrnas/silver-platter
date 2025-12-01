import PageButton from "../Components/PageButton";
import PageLogo from "../Components/PageLogo";


function Header() {
    return (
        <div style={{
            width: "100vw",
            height: "5rem",
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            backgroundColor: "darkslategrey",
            outline: "solid 2px black"
        }}>
            <PageLogo />
            <div style={{
                display: "flex",
                flexDirection: "row",
                alignItems: "center",
                width: "100%",
                height: "100%",
                justifyContent: "center",
                gap: "5%"
            }}>
                <PageButton name="Home" ref="/" />
                <PageButton name="Browse" ref="/Browse" />
                <PageButton name="My Page" ref="/MyPage" />
            </div>
        </div>
    )
}

export default Header;