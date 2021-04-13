<?php 

/**
 * education-x Theme Customizer.
 *
 * @package education-x
 */

//customizer core option
require get_template_directory() . '/inc/customizer/core-customizer.php';

//customizer 
require get_template_directory() . '/inc/customizer/customizer-default.php';
/**
 * Add postMessage support for site title and description for the Theme Customizer.
 *
 * @param WP_Customize_Manager $wp_customize Theme Customizer object.
 */
function education_x_customize_register( $wp_customize ) {

	// Load custom customizer functions.
	require get_template_directory() . '/inc/customizer/customizer-function.php';

	$wp_customize->get_setting( 'blogname' )->transport         = 'postMessage';
	$wp_customize->get_setting( 'blogdescription' )->transport  = 'postMessage';
	
	/*theme option panel details*/
	require get_template_directory() . '/inc/customizer/theme-option.php';


	// Register custom section types.
	$wp_customize->register_section_type( 'Education_X_Customize_Section_Upsell' );

	// Register sections.
	$wp_customize->add_section(
		new Education_X_Customize_Section_Upsell(
			$wp_customize,
			'theme_upsell',
			array(
				'title'    => esc_html__( 'Education X Pro', 'education-x' ),
				'pro_text' => esc_html__( 'Upgrade To Pro', 'education-x' ),
				'pro_url'  => 'http://www.thememattic.com/theme/education-x-pro/',
				'priority'  => 1,
			)
		)
	);

}
add_action( 'customize_register', 'education_x_customize_register' );

/**
 * Binds JS handlers to make Theme Customizer preview reload changes asynchronously.
 *
 * @since 1.0.0
 */
function education_x_customize_preview_js() {

	wp_enqueue_script( 'education_x_customizer', get_template_directory_uri() . '/assets/libraries/js/customizer.js', array( 'customize-preview' ), '20130508', true );

}
add_action( 'customize_preview_init', 'education_x_customize_preview_js' );

function education_x_customizer_css() {
	wp_enqueue_script( 'education_x_customize_controls', get_template_directory_uri() . '/assets/libraries/custom/js/customizer-admin.js', array( 'customize-controls' ) );
}
add_action( 'customize_controls_enqueue_scripts', 'education_x_customizer_css',0 );
