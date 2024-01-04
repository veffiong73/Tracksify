import type { Config } from 'tailwindcss'

const config: Config = {
  content: [
    './pages/**/*.{js,ts,jsx,tsx,mdx}',
    './components/**/*.{js,ts,jsx,tsx,mdx}',
    './app/**/*.{js,ts,jsx,tsx,mdx}',
  ],
  theme: {
    fontFamily: {
      product_sans: "var(--font-product-sans)"
    },
    extend: {
      backgroundImage: {
        'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
        'gradient-conic':
          'conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))',
      },
      colors: {
        'btn_primary': 'var(--color-btn)',
        'text_primary': 'var(--main-text-color)',
        'text_secondary': 'var(--text-color)',
        'text_tertiary': 'var(--text-tertiary)',
      },
      backgroundColor: {
        'btn_background': 'var(--background-btn)',
        'background_primary': 'var(--background)',
        'background_foreground': 'var(--foreground)',
        'color_hover': 'var(--color-hover)',
      }
    },
  },
  plugins: [],
}
export default config
