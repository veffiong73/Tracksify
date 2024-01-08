import Link from "next/link";
import Logo from "./logo";

const NavLinks = {
  employer: [
    {
      name: "Home",
      link: "/employer-dashboard",
    },
    {
      name: "Employee",
      link: "/list-of-employees",
    },
    {
      name: "Project",
      link: "/employer-project",
    },
  ],
  employee: [
    {
      name: "Home",
      link: "/employee-dashboard",
    },
    {
      name: "Project",
      link: "/employee-project",
    },
  ],
};

let userRole = "employer";

const navLinks =
  userRole === "employer" ? NavLinks.employer : NavLinks.employee;
const Navbar = () => {
  return (
    <div className="flex items-center justify-between p-4 bg-white">
      <div className="p-4  ">
        <Logo />
      </div>
      <div>
        {navLinks.map((link) => (
          <Link
            href={link.link}
            key={link.name}
            className=" bg-color_hover rounded-full px-2 py-2 m-2  font-sm  text-text_tertiary hover:text_secondary"
          >
            {link.name}
          </Link>
        ))}
      </div>
      <div>
        {userRole === "employer" && (
          <div className="border-2  border-gray-200 ">
            <button className="bg-text_secondary p-2 m-2   text-white rounded-full">
              FO
            </button>
            <select className="ml-2 outline-none">
              <option value="option1"></option>
              <option value="option2">Option 2</option>
              <option value="option3">Option 3</option>
            </select>
          </div>
        )}
      </div>
    </div>
  );
};

export default Navbar;
